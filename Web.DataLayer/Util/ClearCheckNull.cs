using System.Collections.Generic;
using System.Linq;

namespace Web.DataLayer.Util
{
    public static class ClearCheckNull
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }
}
