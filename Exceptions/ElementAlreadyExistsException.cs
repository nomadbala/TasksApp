using System.Runtime.Serialization;

namespace TasksApp.Exceptions;

public class ElementAlreadyExistsException : Exception
{
    public ElementAlreadyExistsException()
    {
    }

    protected ElementAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ElementAlreadyExistsException(string? message) : base(message)
    {
    }

    public ElementAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}