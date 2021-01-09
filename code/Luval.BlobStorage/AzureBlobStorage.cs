using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luval.BlobStorage
{
    public class AzureBlobStorage : ICloudBlobStorage
    {
        public AzureBlobStorage(string connectionString, string container)
        {
            ServiceClient = new BlobServiceClient(connectionString);
            ContainerClient = ServiceClient.GetBlobContainerClient(container);
            if (ContainerClient == null || !ContainerClient.Exists())
                ContainerClient = ServiceClient.CreateBlobContainer(container);
        }

        protected BlobServiceClient ServiceClient { get; private set; }
        protected BlobContainerClient ContainerClient { get; private set; }

        public async Task DownloadAsync(string blobFullName, Stream stream, CancellationToken cancellationToken)
        {
            var client = ContainerClient.GetBlobClient(blobFullName);
            if (client == null || !client.Exists()) throw new ArgumentException(nameof(blobFullName));
            await client.DownloadToAsync(stream, cancellationToken);
        }

        public async Task DeleteAsync(string blobFullName, CancellationToken cancellationToken)
        {
            var client = ContainerClient.GetBlobClient(blobFullName);
            if (client == null || !(client.Exists(cancellationToken).Value)) throw new ArgumentException(nameof(blobFullName));
            await client.DeleteAsync(cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<IBlobInformation>> GetBlobsAsync(string directoryName, CancellationToken cancellationToken)
        {
            return Task.Run(() => { return GetBlobs(directoryName); }, cancellationToken);
        }

        private IEnumerable<IBlobInformation> GetBlobs(string directoryName)
        {
            var uri = ContainerClient.Uri;
            var result = new List<BlobInformation>();
            var blobs = ContainerClient.GetBlobsByHierarchy(prefix: directoryName, delimiter: "/");
            if (blobs == null || !blobs.Any()) return result;
            return blobs.ToArray().Select(i => BlobInformation.FromCloudBlob(ContainerClient.Uri.ToString(), i)).ToList();
        }

        public Task UploadAsync(string fileName, Stream content, CancellationToken cancellationToken)
        {
            return ContainerClient.UploadBlobAsync(fileName, content, cancellationToken);
        }

        public async Task CreateDirectoryAsync(string currentDirectory, string newDirectoryName, CancellationToken cancellationToken)
        {
            var current = ContainerClient.GetBlobClient(currentDirectory.ToBlobName());
            if (current == null || !current.Exists()) throw new ArgumentException(nameof(currentDirectory));
            var newFile = String.Format("{0}{1}_1.txt", currentDirectory.ToBlobName(), newDirectoryName.ToBlobName());
            using (var stream = new StreamWriter(new MemoryStream()))
            {
                stream.WriteLine("");
                await ContainerClient.UploadBlobAsync(newFile, stream.BaseStream, cancellationToken);
                stream.Close();
            }
            await ContainerClient.DeleteBlobAsync(newFile);
        }
    }
}
