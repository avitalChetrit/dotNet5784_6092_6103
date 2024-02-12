using System.Reflection.Emit;
using System.Xml.Linq;

namespace BO;

public static class Sheduled
{
    public static ScheduleLevel level { get; set; } = ScheduleLevel.Planning;
    public static DateTime? StartDate { get; set; } = null;

    //public override string ToString()
    //{
    //    return "level: " + level + "\n StartDate: " + StartDate;
    //}
}
