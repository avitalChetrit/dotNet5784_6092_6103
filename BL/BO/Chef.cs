using DO;

namespace BO;

/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="Id">Personal unique ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
/// <param name="Email">the Email of the Chef</param>
/// <param name="Level">the Level of the Chef</param>
/// <param name="Cost">the Level of the Chef</param>
/// <param name="Task">the Task (id&Alias) of the Chef</param>

///ישות לוגית ראשית
public class Chef
{
    public int Id {get; init;}
    public string? Name { get; set;}
    public string? Email { get; set; }
    public ChefExperience? Level { get; set; }
    public double? Cost { get; set; }
    public BO.TaskInChef? Task { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n Name: " + Name + "\n Email: " + Email + "\n Level: " + Level + "\n Cost: " + Cost +
               (Task != null ? "\n Task In Chef" + Task : "");
    }
}
