namespace DO;

public record Task
(
    int IdTask,
    string? Alias=null,
    string? Description=null,
    DateTime? CreatedAtDate=null,
    TimeSpan? RequiredTime=null,
    bool IsMilestone=false,
    ChefExperience? Complexity = ChefExperience.Beginner,
    DateTime? StartDate,

)
{
}
