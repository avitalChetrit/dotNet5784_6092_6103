namespace DO;
/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="ChefId">Personal unique ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
/// <param name="Level">the level of the Chef</param>
/// <param name="Cost">the cost of the Chef</param>

public record Chef
(
int ChefId,                 //unique  
ChefExperience? Level = ChefExperience.Beginner,
string? Name = null,
string? Email = null,
double? Cost = null

)
{
    public Chef() : this(0) { }           // empty ctor
}

