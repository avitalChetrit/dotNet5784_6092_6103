namespace BO;
/// <summary>
/// Task In List
/// </summary>
/// <param name="Id">Id </param>
/// <param name="Description">Description</param>
/// <param name="Alias">Alias</param>
/// <param name="Status">Status </param>

public class TaskInList
{
    public int Id { get; init; } 
    public string Description { get; set; }
    public string Alias { get; set; }
    public BO.Status Status { get; set; }    
}

