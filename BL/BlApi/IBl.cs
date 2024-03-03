using System.Numerics;

namespace BlApi;

public interface IBl
{
    public IChef Chef { get; }
    public ITask Task { get; }
    public ISheduled Sheduled { get; }
    public void InitializeDB();
    public void ResetDB();
    #region clock
    public DateTime Clock { get; }
    public void AddYear();
    public void AddDay();
    public void AddHour();
    public void InitializeTime();

    #endregion
}
