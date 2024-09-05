
using System.Security.Cryptography;
using System.Text;
using Common.Extensions;
using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Common.Helper;


public class HashHelper
{
    public static string Argon2Id(string source, string salt = "")
    {
        using var argon = new Argon2id(source.Bytes());
        argon.Salt = salt.Bytes();
        argon.DegreeOfParallelism = 16;
        argon.Iterations = 8;
        argon.MemorySize = 1024 * 128;
        var argonBytes = argon.GetBytes(32);
        var argonBase64 = argonBytes.Base64String();
        return argonBase64;

    }
    public static string Generate(byte[] data)
    {

        using var sha256 = SHA256.Create();
        {
            try
            {
                if (data.IsNullOrEmpty())
                {
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
