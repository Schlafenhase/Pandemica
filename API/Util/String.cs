using System.Collections.Generic;
using System.Linq;

namespace API.Util
{
    public class String
    {
        public static string RemoveChars(IEnumerable<string> chars, string @string)
        {
            return chars.Aggregate(@string, (current, c) => 
                current.Replace(c, string.Empty));
        }
    }
}