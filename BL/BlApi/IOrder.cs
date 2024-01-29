namespace BlApi;
/// <summary>
/// Order interface
/// </summary>
public interface IOrder
{
    public int Create(BO.Order item);
    public BO.Order? Read(int id);
    public IEnumerable<BO.Order> ReadAll();
    public void Update(BO.Order item);
    public void Delete(int id);
}
