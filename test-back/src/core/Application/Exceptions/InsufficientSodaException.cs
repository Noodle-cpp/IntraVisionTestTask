using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class InsufficientSodaException : Exception
{
    public InsufficientSodaException()
    {
    }

    protected InsufficientSodaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InsufficientSodaException(string? message) : base(message)
    {
    }

    public InsufficientSodaException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}