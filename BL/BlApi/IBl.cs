using System.Numerics;

namespace BlApi;

public interface IBl
{
    public IChef Chef { get; }
    public ITask Task { get; }
    public ISheduled Sheduled { get; }

}
