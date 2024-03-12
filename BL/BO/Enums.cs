using System.Diagnostics.Contracts;

namespace BO;

public enum ChefExperience
{
    Beginner=0,
    AdvancedBeginner=1,
    Intermediate=2,
    Advanced=3,
    Expert=4,
}

public enum ChefExperienceFilter
{
    Beginner = 0,
    AdvancedBeginner = 1,
    Intermediate = 2,
    Advanced = 3,
    Expert = 4,
    All = 5
}

public enum Status
{
    UnScheduled,  //no ScheduledDate date (ScheduledDate=null)
    Scheduled,    //(ScheduledDate != null)
    OnTrack,      //(Start != null)
    Done,          //CompleteDate != 
    All
}

public enum ScheduleLevel
{
    Planning,
    Mid,
    Execution
}

