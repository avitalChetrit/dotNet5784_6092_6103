namespace DalApi;
using DO;

public interface ISchedule
{
    void Update(DateTime? dateTime);
    DateTime? Read();
    void Delete();
}

