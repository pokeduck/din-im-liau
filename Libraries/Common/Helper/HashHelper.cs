
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Common.Helper;


public class HashHelper
{
    public static string Generate(byte[] data)
    {

        using var sha256 = SHA256.Create();
        {
            try
            {
                if (data.IsNullOrEmpty()) {
                    return "";
                }
                var result = sha256.ComputeHash(data);
                var resultString = StringFromByteArray(result);
                return resultString;
            }
            catch 
            {
                return "";
            }
        }
    }

    // Display the byte array in a readable format.
    private static string StringFromByteArray(byte[] array)
    {
        var result = "";
        for (var i = 0; i < array.Length; i++)
        {
            var currentElement = $"{array[i]:x2}";
            result += currentElement;
            // if ((i % 4) == 3)
            //     result += " ";
        }
        return result;
    }
}
