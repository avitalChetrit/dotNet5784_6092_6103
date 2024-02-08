namespace BlImplementation;
using BlApi;
using Task = BO.Task;
internal class Bl : IBl
{
    public IChef Chef => new ChefImplementation();

    public ITask Task => new TaskImplementation();

    public ISheduled Sheduled => new SheduledImplementation();

    //public DateTime Start => new StartImplementation();
}