namespace BlApi;

/// <summary>
/// Task interface
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null); //stage 4
    public void Update(BO.Task item);
    public void Delete(int id);
    public void UpdateDate(int id, DateTime d);
    public BO.Status findStat(int id);

}
