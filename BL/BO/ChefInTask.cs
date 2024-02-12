using System.Reflection.Emit;

namespace BO;
/// <summary>
/// ChefInTask
/// </summary>
/// <param name="Id">Personal unique ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
public class ChefInTask
{
    public int Id { get; init; } 
    public string Name { get; set; }

    public override string ToString()
    {
        return "Id: " + Id + "\n Name: " + Name;
    }
    //public override string ToString() => this.ToStringProperty();
}
