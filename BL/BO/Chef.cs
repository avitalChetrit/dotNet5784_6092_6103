using DO;

namespace BO;

/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="Id">Personal unique ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
/// <param name="Level">the level of the Chef</param>
/// <param name="Cost">the cost of the Chef</param>
public class Chef
{
    public int Id {get; init;}
    public ChefExperience? Level { get; set; }
    public string? Name { get; set;}
    public string? Email { get; set; }
    public double? Cost { get; set; }
    public ChefSpeciality? Speciality { get; set; }

    //public override string ToString() => this.ToStringProperty();
}
