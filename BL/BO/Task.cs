namespace BO;

/// <summary>
/// Class for Task
/// </summary>

public class Task
{
    public int Id { get; init; } 
    public string? Description { get; set; }
    public string? Alias { get; set; }  
    public DateTime? CreatedAtDate { get; set; }
    public BO.Status Status { get; set; }   

    //public List<BO.TaskInList> Dependecies { get; set; }
    
    //public BO.MilestoneInTask Milestone {  get; set; }  

    public TimeSpan RequiredEffortTime { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime SchedueldDate { get; set; }
    public DateTime ForecastDate { get; set; }
    public DateTime DeadlineDate { get; set; }
    public DateTime CompleteDate { get; set; }
    public string Deliverables { get; set; }
    public string Remarks { get; set; }
    
    public BO.ChefInTask Chef { get; set; }    
    public BO.ChefExperience Complexity { get; set; }
};
