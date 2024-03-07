using BlApi;
using BO;
using DalApi;
namespace BlImplementation;

public class ScheduleImplementation : ISchedule
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
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
}
