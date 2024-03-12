using BO;
namespace BlApi;

public interface ISchedule
{
    void Update(DateTime? dateTime);
    DateTime? Read();
    void Delete();

    //A method that will return the level of the status of the project
    public ScheduleLevel levelStatuas();
}
