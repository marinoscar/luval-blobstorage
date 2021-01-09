using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.BlobStorage
{
    public static class Extensions
    {
        public static string ToBlobName(this object o)
        {
            if (o == null || DBNull.Value.Equals(o)) return null;
            var s = Convert.ToString(o);
            return s.EndsWith("/") ? s : s + "/";
        }
    }
}
