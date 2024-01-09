namespace DO;

// The object doesn't exist( use in upade, delete and read)
[Serializable]
public class DalDoesNotExistException : Exception 
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

// The object already exist( use in create)
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

// The object can't be deleted( use in delete)
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}
