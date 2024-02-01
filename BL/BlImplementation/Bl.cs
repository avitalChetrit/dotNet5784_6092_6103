namespace BlImplementation;
using BlApi;
internal class Bl : IBl
{
    public IChef Chef => new ChefImplementation();

    public ITask Task => new TaskImplementation();
}