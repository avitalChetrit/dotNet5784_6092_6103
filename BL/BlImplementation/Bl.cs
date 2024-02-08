namespace BlImplementation;
using BlApi;
using Task = BO.Task;
internal class Bl : IBl
{
    public IChef Chef => new ChefImplementation();

    public ITask Task => new TaskImplementation();

    public I

    //public DateTime Start => new StartImplementation();
}