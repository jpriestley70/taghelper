using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperDemo.Library
{
    public static class StringExtension
    {
        /// <summary>
        /// Is the string NULL, empty or just contains white spaces?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Return number of times a text sequence is in a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int CountOf(this string text, string pattern)
        {
            if (text.IsEmpty()) { return 0; }
            if (pattern.IsEmpty()) { return 0; }

            int count = 0;
            int index = -1;
            bool found = true;

            try
            {
                while (found == true)
                {
                    if ((index + 1) >= text.Length) { break; }

                    found = false;
                    int newIndex = text.IndexOf(pattern, index + 1, StringComparison.InvariantCultureIgnoreCase);
                    if (newIndex > index)
                    {
                        found = true;
                        index = newIndex;
                        count++;
                    }
                }
            }
            catch
            {
                // In case index is greater than string length
            }

            return count;
        }
    }
}
