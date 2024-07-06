using System.Runtime.Serialization;

namespace TasksApp.Exceptions;

public class ElementNotFoundException : Exception
{
    public ElementNotFoundException()
    {
    }

    protected ElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ElementNotFoundException(string? message) : base(message)
    {
    }

    public ElementNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}