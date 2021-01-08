using System;

namespace Luval.BlobStorage
{
    public interface IBlobInformation : IBlobFileName
    {
        string ContentEncoding { get; }
        long? ContentLength { get; }
        string ContentType { get; }
        DateTimeOffset? CreatedOn { get; }
        DateTimeOffset? UpdatedOn { get; }
        Uri Url { get; }
    }
}