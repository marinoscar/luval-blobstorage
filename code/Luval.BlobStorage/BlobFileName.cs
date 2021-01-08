using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.BlobStorage
{
    internal class BlobFileName : IBlobFileName
    {
        public string DirectoryName { get; set; }
        public string FullName { get; set; }
        public string FileName { get; set; }

    }
}
