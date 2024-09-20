namespace Coordinator.Models;

public class Node
{
    public Guid Id { get; set; } 
    public string Name { get; set; } // This must match the constructor
 public ICollection<NodeState> NodeStates { get; set; } 
     public Node(string name)
    {
        Name = name;
    }
}