using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class CartNotFoundException : Exception
{
    public CartNotFoundException()
    {
    }

    protected CartNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CartNotFoundException(string? message) : base(message)
    {
    }

    public CartNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}