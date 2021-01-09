using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.BlobStorage.Web.Models
{
    public class FileModel
    {
        public FileModel()
        {

        }

        public FileModel(IBlobInformation blob)
        {
            Name = blob.FileName;
            FullName = blob.FullName;
            Url = blob.Url.ToString();
            FileType = blob.ContentType;
            if(blob.ContentLength != null)
                Size = GetSize(blob.ContentLength.Value);
            if (blob.UpdatedOn != null)
                ModifiedOn = blob.UpdatedOn.Value.ToString("MMM dd, yy");
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Url { get; set; }
        public string ModifiedOn { get; set; }
        public string FileType { get; set; }
        public string Size { get; set; }

        private static string GetSize(long val)
        {
            if (val < (Math.Pow(1000,2)))
                return (val / (double)(1000)).ToString("N2") + " KB";
            if(val < (Math.Pow(1000, 4)))
                return (val / Math.Pow(1000, 2)).ToString("N2") + " MB";
            return (val / Math.Pow(1000, 3)).ToString("N2") + " GB";

        }
    }
}
