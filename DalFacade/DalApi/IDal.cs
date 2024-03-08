namespace DalApi;

public interface IDal
{
    IChef Chef { get; }
    ITask Task { get; }
    IDependency Dependency { get; }
    ISchedule Schedule { get; }
}
