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
        Task CreateDirectoryAsync(string currentDirectory, string newDirectoryName, CancellationToken cancellationToken);
        Task<IEnumerable<IBlobInformation>> GetBlobsAsync(string directoryName, CancellationToken cancellationToken);
        Task DownloadAsync(string blobFullName, Stream stream, CancellationToken cancellationToken);
        Task DeleteAsync(string blobFullName, CancellationToken cancellationToken);
    }
}
