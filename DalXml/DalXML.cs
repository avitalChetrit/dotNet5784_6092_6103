using DalApi;
using System.Diagnostics;
namespace Dal;

//stage 3
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IChef Chef => new ChefImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
}
