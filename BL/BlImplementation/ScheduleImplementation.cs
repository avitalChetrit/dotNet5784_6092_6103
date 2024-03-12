using BlApi;
using BO;

namespace BlImplementation;

public class ScheduleImplementation : ISchedule
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Delete()
    {
        _dal.Schedule.Delete();
    }

    public ScheduleLevel levelStatuas()
    {
        if (Schedule.level == ScheduleLevel.Mid) { return ScheduleLevel.Mid; }

        if (Schedule.StartDate == null) { Schedule.level = ScheduleLevel.Planning; return ScheduleLevel.Planning; }

        else
        {
            bool AllhaveScheduledDate = _dal.Task.ReadAll().All(item => item!.ScheduledDate != null);

            if (!AllhaveScheduledDate) { Schedule.level = ScheduleLevel.Mid; return ScheduleLevel.Mid; }
            else { Schedule.level = ScheduleLevel.Mid; return ScheduleLevel.Execution; }
        }
    }

    public DateTime? Read()
    {
        return _dal.Schedule.Read();
    }

    public void Update(DateTime? dateTime)
    {
        Schedule.StartDate = dateTime;
        _dal.Schedule.Update(dateTime); 
    }
}
