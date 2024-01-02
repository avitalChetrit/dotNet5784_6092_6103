namespace DO;
/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="ChefId">Personal random ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
/// <param name="Email">Private Email of the Chef</param>
/// <param name="Cost">the cost of the Chef</param>

public record Chef
(
int ChefId,                 //random 
string? Name = null,
string? Email = null,
double? Cost = null,

ChefExperience Level = ChefExperience.Beginner
//add ? to nullable type

);


//{
//public Chef() : this(0) { }
//}

