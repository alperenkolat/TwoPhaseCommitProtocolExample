using Coordinator.Models.Context;
using Coordinator.Services;
using Coordinator.Services.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TwoPhaseCommitContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
builder.Services.AddTransient<ITransactionService, TransactionService>();


builder.Services.AddHttpClient( "OrderAPI",
    client=>client.BaseAddress = new Uri("http://localhost:5165"));

builder.Services.AddHttpClient( "StockAPI",
    client=>client.BaseAddress = new Uri("http://localhost:5073"));

builder.Services.AddHttpClient( "PaymentAPI",
    client=>client.BaseAddress = new Uri("http://localhost:5074"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/create-order-transaction", async (ITransactionService transactionService) =>
{
    var transactionId = await transactionService.CreateTransactionAsync();
    await transactionService.PrepareServicesAsync(transactionId);
    bool transactionState = await transactionService.CheckReadyServicesAsync(transactionId);
    if (transactionState)
    {
        await transactionService.CommitAsync(transactionId);
        transactionState = await transactionService.CheckTransactionStateServicesAsync(transactionId);
    }

    if (!transactionState)
        await transactionService.RollbackAsync(transactionId);
  

});


app.Run();

