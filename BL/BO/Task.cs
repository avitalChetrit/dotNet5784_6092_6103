using System.Xml.Linq;

namespace BO;

/// <summary>
/// Class for Task
/// </summary>
/// <param name="Id">Id </param>
/// <param name="Description">Description</param>
/// <param name="Alias">Alias</param>
/// <param name="CreatedAtDate">Creation Date of task</param>
/// <param name="Status">Status</param>
/// <param name="Dependecies">List of Dependecies</param>
/// <param name="RequiredEffortTime">Required Effort Time</param>
/// <param name="StartDate">Start Date, recieve value when its picked by chef</param>
/// <param name="SchedueldDate">Schedueld Date, recieve value by Manager</param>
/// <param name="ForecastDate">תאריך משוער לסיום - שדה מחושב: המקסימום מבין תאריך ההתחלה המתוכנן ותאריך ההתחלה בפועל + משך המטלה </param>
/// <param name="CompleteDate">Complete Date,recieve value when chef finishes task </param>
/// <param name="Deliverables">תוצר</param>
/// <param name="Remarks">הערות</param>
/// <param name="Chef"></param>
/// <param name="Complexity"></param>
public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; } 
    public string? Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; } 
    public BO.Status Status { get; set; }
    public List<BO.TaskInList>? Dependecies { get; set; } = null;
    public TimeSpan? RequiredTime { get; set; } 
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ForecastDate { get; set; }//הערכת סיום
    public DateTime? CompleteDate { get; set; } 
    public string? Deliveables { get; set; } 
    public string? Remarks { get; set; } 
    public BO.ChefInTask? Chef { get; set; }
    public BO.ChefExperience Complexity { get; set; }

    //Milestone
    //public DateTime DeadlineDate { get; set; } = DeadlineDate; 
    //public BO.MilestoneInTask Milestone {  get; set; }  

    public override string ToString()
    {
        return "Id: " + Id + "\n Description: " + Description + "\n Alias: " + Alias + "\n CreatedAtDate: " + CreatedAtDate
                           + "\n Status: " + Status + "\n Dependecies: " + Dependecies + "\n RequiredTime: " + RequiredTime
                           + "\n StartDate: " + StartDate + "\n ScheduledDate: " + ScheduledDate + "\n ForecastDate: " + ForecastDate
                           + "\n CompleteDate: " + CompleteDate + "\n Deliveables: " + Deliveables + "\n Remarks: " + Remarks
                           + "\n Chef: " + Chef + "\n Complexity: " + Complexity;

    }
}