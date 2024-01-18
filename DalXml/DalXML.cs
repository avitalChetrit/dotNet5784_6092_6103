using DalApi;
namespace Dal;

//stage 3
sealed public class DalXml : IDal
{
    public IChef Chef => new ChefImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
}
