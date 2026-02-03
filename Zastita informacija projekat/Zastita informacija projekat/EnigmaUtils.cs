using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zastita_informacija_projekat
{
    internal static class EnigmaUtils
    {
        public static int[] StringToIntArray(string text)
        {
            return text.ToUpper().Where(c => char.IsLetter(c))
                       .Select(c => c - 'A').ToArray();
        }

        public static string IntArrayToString(int[] array)
        {
            if (array == null) return "";
            var sb = new StringBuilder();
            foreach (int i in array) sb.Append((char)('A' + i));
            return sb.ToString();
        }
    }
}
