using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.BlobStorage
{
    public interface IBlobFileName
    {
        string FullName { get; }
        string FileName { get; }
        string DirectoryName { get; }
    }
}
