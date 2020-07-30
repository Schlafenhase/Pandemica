using System.Collections.Generic;
using System.Linq;

namespace API.Util
{
    public class String
    {
        /// <summary>
        /// Function in charge of removing characters from a string
        /// </summary>
        /// <param name="chars">
        /// Chars to be removed
        /// </param>
        /// <param name="string">
        /// String that will have the chars removed
        /// </param>
        /// <returns>
        /// String with the chars removed
        /// </returns>
        public static string RemoveChars(IEnumerable<string> chars, string @string)
        {
            return chars.Aggregate(@string, (current, c) => 
                current.Replace(c, string.Empty));
        }
    }
}