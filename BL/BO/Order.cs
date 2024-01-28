namespace BO;

public class Order
{
    public int ChefId { get; init; }
    public string? ChefName { get; init; }
    public Customer? Client { get; init; }
    public List<Task>? Tasks { get; init; } = null; 
    //public override string ToString() => this.ToStringProperty();
}
