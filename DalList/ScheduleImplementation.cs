namespace Dal;
using DalApi;
using DO;
using System;

public class ScheduleImplementation : ISchedule
{
    public void Delete()
    {
        DataSource.StartDate = null;
    }

    public DateTime? Read()
    {
        return DataSource.StartDate;    
    }

    public void Update(DateTime? dateTime)
    {
        DataSource.StartDate = dateTime;
    }
}
