using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class CoinNotFoundException : Exception
{
    public CoinNotFoundException()
    {
    }

    protected CoinNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CoinNotFoundException(string? message) : base(message)
    {
    }

    public CoinNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}