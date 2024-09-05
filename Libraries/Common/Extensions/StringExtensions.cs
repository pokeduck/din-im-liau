using System.Text;

namespace Common.Extensions;

public static class StringExtensions
{
    public static byte[] Bytes(this string value)
    {
        return Encoding.UTF8.GetBytes(value);
    }
}
