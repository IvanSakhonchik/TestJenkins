using System;
using System.Text;

namespace RestAPI
{
    public static class StringUtil
    {
        public static string GetRandomString(int length = 5)
        {
            if (length <= 0)
            {
                throw new Exception("Incorrect size for random string");
            }
            Random random = new Random();
            var builder = new StringBuilder(length);
            const int lettersOffset = 26;
            for (var i = 0; i < length; i++)
            {
                var element = (char)random.Next('a', 'a' + lettersOffset);
                builder.Append(element);
            }
            return builder.ToString();
        }
    }
}
