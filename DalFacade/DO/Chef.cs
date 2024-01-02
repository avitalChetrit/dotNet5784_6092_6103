namespace DO;

/// <summary>
////// Chef Entity represents a Chef with all its props
/// </summary>

public record Chef
{   
    int Id;//unique 
    string Name;
    string? Email=null;
    double Cost;
    
    ChefExperience Level; 
    //add ? to nullable type
}
