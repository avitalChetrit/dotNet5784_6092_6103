namespace DO;
/// <summary>
/// Task Entity represents a Task with all its props
/// </summary>
/// <param name="Id">Personal random ID of the Task </param>
/// <param name="Alias">Name of the Task </param>
/// <param name="Description">Description of the Task </param>
/// <param name="IsMilestone">If Task is a Milestone </param>
/// <param name="Complexity">What Experience level the chef needs to be </param>
/// <param name="CreatedAtDate">When was the task first created </param>
/// <param name="RequiredTime">How much time the task requires </param>


public record Task
(
    int Id,
    string? Alias=null,
    string? Description=null,
    bool IsMilestone = false,
    ChefExperience? Complexity = ChefExperience.Beginner,

    DateTime? CreatedAtDate=null,  
    TimeSpan? RequiredTime=null,
    DateTime? StartDate=null,
    DateTime? ScheduledDate=null,
    DateTime? DeadLineDate=null,
    DateTime? CompleteDate = null,
    string? Deliveables=null,
    string? Remarks=null,
    int? ChefId=null
)
{
    public Task() : this(0) { }           // empty ctor
}
