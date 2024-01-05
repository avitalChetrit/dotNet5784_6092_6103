namespace DO;
/// <summary>
/// Dependency Entity represents a Dependency with all its props
/// </summary>
/// <param name="Id">Personal random ID of the Dependency </param>
/// <param name="preTask">Name of the previous Task</param>
/// <param name="currTask">Name of the current Task</param>

public record Dependency
(
    int Id, //random
    int? preTask=null,
    int? currTask = null     //depend on the previous task
)
{
    public Dependency() : this(0) { }           // empty ctor
}
