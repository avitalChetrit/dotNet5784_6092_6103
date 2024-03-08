namespace BlImplementation;
using BlApi;
using Task = BO.Task;
internal class Bl : IBl
{
    public IChef Chef => new ChefImplementation();

    public ITask Task => new TaskImplementation(this);

    public ISchedule Schedule => new ScheduleImplementation();

    public void InitializeDB() => DalTest.Initialization.Do();

    public void ResetDB() => DalTest.Initialization.Reset();

    private static DateTime s_Clock = DateTime.Now.Date;
    public DateTime Clock { get { return s_Clock; } private set { s_Clock = value; } }

    public void AddYear()
    {
        s_Clock = s_Clock.AddYears(1);
    }

    public void AddDay()
    {
        s_Clock = s_Clock.AddDays(1);
    }

    public void AddHour()
    {
        s_Clock = s_Clock.AddHours(1);
    }

    public void InitializeTime()
    {
        s_Clock = DateTime.Now.Date;
    }


    //public DateTime Start => new StartImplementation();
}