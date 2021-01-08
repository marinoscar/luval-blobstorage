using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luval.BlobStorage
{
    internal class BlobInformation : BlobFileName, IBlobInformation
    {
        public bool IsDirectory { get; set; }
        public Uri Url { get; set; }
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public string ContentEncoding { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public static IBlobInformation FromCloudBlob(string rootUrl, BlobHierarchyItem b)
        {
            if(b.IsPrefix)
                return new BlobInformation()
                {
                    Url = new Uri(string.Format("{0}/{1}", rootUrl, b.Prefix)),
                    FullName = b.Prefix,
                    DirectoryName = b.Prefix,
                    IsDirectory = b.IsPrefix,
                };
            var parts = b.Blob.Name.Split('/');
            var name = parts.Last();
            var dirName = string.Join("/", parts.Skip(parts.Length - 1));
            return new BlobInformation()
                    {
                        Url = new Uri(string.Format("{0}/{1}", rootUrl, b.Blob.Name)),
                        FileName = name,
                        FullName = b.Blob.Name,
                        ContentEncoding = b.Blob.Properties.ContentEncoding,
                        ContentLength = b.Blob.Properties.ContentLength,
                        ContentType = b.Blob.Properties.ContentType,
                        DirectoryName = dirName,
                        IsDirectory = b.IsPrefix,
                        CreatedOn = b.Blob.Properties.CreatedOn,
                        UpdatedOn = b.Blob.Properties.LastModified
                    };
        }
    }
}
