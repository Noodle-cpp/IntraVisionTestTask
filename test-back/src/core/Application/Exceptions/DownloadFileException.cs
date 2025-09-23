using System.Runtime.Serialization;

namespace Application.Exceptions;

[Serializable]
public class DownloadFileException : Exception
{
    public DownloadFileException()
    {
    }

    protected DownloadFileException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DownloadFileException(string? message) : base(message)
    {
    }

    public DownloadFileException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}