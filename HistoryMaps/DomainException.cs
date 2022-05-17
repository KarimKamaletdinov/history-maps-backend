namespace HistoryMaps;

public class DomainException : Exception
{
    public DomainException(string text) : base(text)
    {

    }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string text) : base(text)
    {

    }
}

public class DoesNotExistException : Exception
{
    public DoesNotExistException(string text) : base(text)
    {

    }
}