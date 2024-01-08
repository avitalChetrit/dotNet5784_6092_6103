namespace DO;
/// <summary>
/// Dependency Entity represents a Dependency with all its props
/// </summary>
/// <param name="Id">Personal random ID of the Dependency </param>
/// <param name="preTask">ID of the previous Task</param>
/// <param name="currTask">ID of the current Task</param>

public record Dependency
(
    int Id, //random
    int? PreTask=null,      //depend on the previous task
    int? CurrTask = null     
)
{
    public Dependency() : this(0) { }           // empty ctor
}
