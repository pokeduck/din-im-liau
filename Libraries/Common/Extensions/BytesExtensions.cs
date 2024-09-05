using Microsoft.VisualBasic;

namespace Common.Extensions;

public static class BytesExtensions
{
    public static string Base64String(this byte[] source)
    {
        var result = Convert.ToBase64String(source);
        return result;
    }
}
