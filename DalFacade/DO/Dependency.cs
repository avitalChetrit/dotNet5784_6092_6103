namespace DO;
/// <summary>
/// Chef Entity represents a Chef with all its props
/// </summary>
/// <param name="Id">Personal random ID of the Dependency </param>
/// <param name="Name">Private Name of the Dependency</param>
/// <param name="Cost">the cost of the Dependency</param>

public record Dependency
(
    int Id, //random
    int? preTask=null,
    int? currTask = null     //depend on the previous task
)
{
    public Dependency() : this(0) { }           // empty ctor
}
