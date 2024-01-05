namespace DO;

public record Task
(
    int IdTask,
    string? Alias=null,
    string? Description=null,
    DateTime? CreatedAtDate=null,
    TimeSpan? RequiredTime=null,
    bool IsMilestone=false,
    ChefExperience? Complexity = ChefExperience.Beginner,  //חדש enumלא צריך כאן ?
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
