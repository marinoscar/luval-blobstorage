using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.BlobStorage.Web
{
    public static class BlobStorageServiceCollectionExtensions
    {
        public static void AddBlobStorage(this IServiceCollection services, string azureConnectionString, string storageContainer)
        {
            var blobStorage = new AzureBlobStorage(azureConnectionString, storageContainer);
            services.ConfigureOptions(typeof(BlobStorageConfigureOptions));
            services.AddSingleton<ICloudBlobStorage>(blobStorage);
        }
    }
}
