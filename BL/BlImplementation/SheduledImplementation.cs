using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class SheduledImplementation : ISheduled
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public ScheduleLevel levelStatuas()
    {
        if(Sheduled.level == ScheduleLevel.Mid) { return ScheduleLevel.Mid; }

        if (Sheduled.StartDate == null) { Sheduled.level= ScheduleLevel.Planning;  return ScheduleLevel.Planning; }

        else
        {
            bool AllhaveScheduledDate = _dal.Task.ReadAll().All(item => item!.ScheduledDate != null);

            if (!AllhaveScheduledDate) { Sheduled.level = ScheduleLevel.Mid;  return ScheduleLevel.Mid; }
            else { Sheduled.level = ScheduleLevel.Mid; return ScheduleLevel.Execution; }
        }
    }
}
