using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException()
    {
    }

    protected InsufficientFundsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InsufficientFundsException(string? message) : base(message)
    {
    }

    public InsufficientFundsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}