using System.Reflection.Emit;
using System.Xml.Linq;

namespace BO;
/// <summary>
/// TaskInChef
/// </summary>
/// <param name="Id">Personal random ID of the Task </param>
/// <param name="Alias">Name of the Task </param>
public class TaskInChef
{
    public int Id { get; init; }
    public string Alias { get; set; }
    public override string ToString()
    {
        return "Id: " + Id + "\n Alias: " + Alias;
    }
}

