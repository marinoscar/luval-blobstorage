using System;

namespace Luval.BlobStorage
{
    public interface IBlobInformation : IBlobFileName
    {
        bool IsDirectory { get; }
        string ContentEncoding { get; }
        long? ContentLength { get; }
        string ContentType { get; }
        DateTimeOffset? CreatedOn { get; }
        DateTimeOffset? UpdatedOn { get; }
        Uri Url { get; }
    }
}