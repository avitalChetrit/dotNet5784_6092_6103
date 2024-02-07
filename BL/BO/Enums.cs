namespace BO;

public enum ChefExperience
{
    Beginner=0,
    AdvancedBeginner=1,
    Intermediate=2,
    Advanced=3,
    Expert=4
}

public enum Status
{
    Unscheduled,  //no ScheduledDate date (ScheduledDate=null)
    Scheduled,    //(ScheduledDate != null)
    OnTrack,      //(Start != null)
    Done          //CompleteDate != 
}
