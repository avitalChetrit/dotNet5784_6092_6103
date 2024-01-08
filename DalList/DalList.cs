namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public IChef Chef => new ChefImplementation();


    public ITask Task => new TaskImplementation();


    public IDependency Dependency => new DependencyImplementation();

}
