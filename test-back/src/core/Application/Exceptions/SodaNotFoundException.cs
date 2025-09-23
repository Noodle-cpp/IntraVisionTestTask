using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class SodaNotFoundException : Exception
{
    public SodaNotFoundException()
    {
    }

    protected SodaNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SodaNotFoundException(string? message) : base(message)
    {
    }

    public SodaNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}