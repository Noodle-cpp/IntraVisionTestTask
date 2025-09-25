using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class ChangeCoinException : Exception
{
    public ChangeCoinException()
    {
    }

    protected ChangeCoinException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ChangeCoinException(string? message) : base(message)
    {
    }

    public ChangeCoinException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}