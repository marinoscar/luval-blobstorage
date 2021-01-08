using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.BlobStorage
{
    public interface ICloudBlobStorage
    {
        Task UploadAsync(string fileName, Stream content, CancellationToken cancellationToken);
        Task<IEnumerable<IBlobInformation>> GetBlobsAsync(string directoryName, CancellationToken cancellationToken);
        Task DownloadAsync(IBlobInformation blob, Stream stream, CancellationToken cancellationToken);
        Task DeleteAsync(IBlobInformation blob, CancellationToken cancellationToken);
    }
}
