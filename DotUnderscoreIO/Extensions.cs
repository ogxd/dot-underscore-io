using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogx {

    public static class Extensions {

        public static string SetLength(this string value, int length) {
            if (string.IsNullOrEmpty(value))
                return new string(' ', length);
            return value.Length > length ? value.Substring(0, length) : value + new string(' ', length - value.Length);
        }

        public static int GetPadding(int size) {
            int padding = 0;
            if (size % 4 > 0)
                padding = 4 - size % 4;
            return padding;
        }
    }
}
