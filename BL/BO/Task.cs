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
public record Task(int Id, string? Description, string? Alias, DateTime? CreatedAtDate, BO.Status Status, TimeSpan RequiredEffortTime, DateTime StartDate, DateTime SchedueldDate, DateTime ForecastDate, DateTime DeadlineDate, DateTime CompleteDate, string Deliverables, string Remarks, BO.ChefInTask Chef, BO.ChefExperience Complexity)
{
    public string? Description { get; set; } = Description;
    public string? Alias { get; set; } = Alias;
    public DateTime? CreatedAtDate { get; set; } = CreatedAtDate;
    public BO.Status Status { get; set; } = Status;
    public List<BO.TaskInList> Dependecies { get; set; }
    //Milestone
    //public BO.MilestoneInTask Milestone {  get; set; }  

    public TimeSpan RequiredEffortTime { get; set; } = RequiredEffortTime;
    public DateTime StartDate { get; set; } = StartDate;
    public DateTime SchedueldDate { get; set; } = SchedueldDate;
    public DateTime ForecastDate { get; set; } = ForecastDate;
    //Milestone
    //public DateTime DeadlineDate { get; set; } = DeadlineDate; 
    public DateTime CompleteDate { get; set; } = CompleteDate;
    public string Deliverables { get; set; } = Deliverables;
    public string Remarks { get; set; } = Remarks;
    public BO.ChefInTask? Chef { get; set; } = Chef;
    public BO.ChefExperience Complexity { get; set; } = Complexity;
}