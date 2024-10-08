using Coordinator.Models;
using Coordinator.Models.Context;
using Coordinator.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Coordinator.Services;

public class TransactionService(TwoPhaseCommitContext _context,IHttpClientFactory HttpClientFactory):ITransactionService 

{
    private readonly HttpClient _orderHttpClient = HttpClientFactory.CreateClient("OrderAPI");
    private readonly HttpClient _stockHttpClient = HttpClientFactory.CreateClient("StockAPI");
    private readonly HttpClient _paymentHttpClient = HttpClientFactory.CreateClient("PaymentAPI");

    public async Task<Guid> CreateTransactionAsync()
    {
        
        Guid transactionId = Guid.NewGuid();

        var nodes = await _context.Nodes.ToListAsync();
        nodes.ForEach(node => node.NodeStates = new List<NodeState>
        {
            new NodeState
            {
                TransactionId = transactionId,
                IsReady = Enums.ReadyType.Pending,
                TransactionState = Enums.TransactionState.Pending
            }
        });

        await _context.SaveChangesAsync();
    
        return transactionId;
    }
    
    public  async Task PrepareServicesAsync(Guid transactionId)
    {
        var transactionNodes = await _context.NodeStates
            .Include(ns => ns.Node)
            .Where(ns => ns.TransactionId == transactionId)
            .ToListAsync();

        foreach (var transactionNode in transactionNodes)
        {
            try
            {
                var response = await (transactionNode.Node.Name switch
                {
                    "Order.API" => _orderHttpClient.GetAsync("ready"),
                    "Stock.API" => _stockHttpClient.GetAsync("ready"),
                    "Payment.API" => _paymentHttpClient.GetAsync("ready"),
                    _ => throw new ArgumentException("Unknown API")
                });

                var result = bool.Parse(await response.Content.ReadAsStringAsync());
                transactionNode.IsReady = result ? Enums.ReadyType.Ready : Enums.ReadyType.Unready;
            }
            catch 
            {
                transactionNode.IsReady = Enums.ReadyType.Unready;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> CheckReadyServicesAsync(Guid transactionId)
        => (await _context.NodeStates
            .Where(ns => ns.TransactionId == transactionId).ToListAsync())
            .TrueForAll(ns => ns.IsReady == Enums.ReadyType.Ready);

    public async Task CommitAsync(Guid transactionId)
    {
        var transactionNodes = await _context.NodeStates.Where(ns => ns.TransactionId == transactionId)
            .Include(ns => ns.Node)
            .ToListAsync();

        foreach (var transactionNode in transactionNodes)
        {
            try
            {
                var response= await (transactionNode.Node.Name switch
                {
                    "Order.API" => _orderHttpClient.GetAsync("commit"),
                    "Stock.API" => _stockHttpClient.GetAsync("commit"),
                    "Payment.API" => _paymentHttpClient.GetAsync("commit"),
                });
                var result =bool.Parse(await response.Content.ReadAsStringAsync());
                transactionNode.TransactionState =
                    result ? Enums.TransactionState.Done : Enums.TransactionState.Aborted;
            }
            catch
            {
                transactionNode.TransactionState = Enums.TransactionState.Aborted;
            }

            await _context.SaveChangesAsync();
        }

    }

    public async Task<bool> CheckTransactionStateServicesAsync(Guid transactionId)
        => (await _context.NodeStates
                .Where(ns => ns.TransactionId == transactionId).ToListAsync())
            .TrueForAll(ns => ns.TransactionState == Enums.TransactionState.Done);   
    
    

    public async Task RollbackAsync(Guid transactionId)
    {
        var transactionNodes = await _context.NodeStates.Where(ns => ns.TransactionId == transactionId)
            .Include(ns => ns.Node)
            .ToListAsync();

        foreach (var transactionNode in transactionNodes)
        {
            try
            {
              _ = await (transactionNode.Node.Name switch
                    {
                        "Order.API" => _orderHttpClient.GetAsync("rollback"),
                        "Stock.API" => _stockHttpClient.GetAsync("rollback"),
                        "Payment.API" => _paymentHttpClient.GetAsync("rollback"),
                    });
                    transactionNode.TransactionState = Enums.TransactionState.Aborted;
                
            }
            catch
            {
                transactionNode.TransactionState = Enums.TransactionState.Aborted;
            }

            await _context.SaveChangesAsync();
        }
        
    }
}
