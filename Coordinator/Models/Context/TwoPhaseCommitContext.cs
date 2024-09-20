using Coordinator.Enums;
using Microsoft.EntityFrameworkCore;

namespace Coordinator.Models.Context;

public class TwoPhaseCommitContext: DbContext
{
  public TwoPhaseCommitContext(DbContextOptions options) : base(options)
  {
    
    
  }

  public DbSet<Node> Nodes { get; set; }
  public DbSet<NodeState> NodeStates { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Node>().HasData(
      new Node(GlobalEnum.OrderApi) { Id =  Guid.NewGuid()} ,
      new Node(GlobalEnum.StockApi) { Id =  Guid.NewGuid()} ,
      new Node(GlobalEnum.PaymentApi)  { Id =  Guid.NewGuid()} 
    );

  }   
  
  
}