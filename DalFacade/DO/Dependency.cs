namespace DO;
/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="ChefId">Personal random ID of the Chef </param>
/// <param name="Name">Private Name of the Chef</param>
/// <param name="Email">Private Email of the Chef</param>
/// <param name="Cost">the cost of the Chef</param>

public record Dependency
(
    int Id,
    int? preTask=null,
    int? currTask = null
)
{
    public Dependency() : this(0) { }           // empty ctor
}
