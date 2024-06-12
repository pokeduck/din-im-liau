

using System.Security.Cryptography;

namespace Common.Helper;
public class RandomHelper{
    public static string Random(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var stringChars = new char[length];
        var randomBytes = new byte[length];

        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(randomBytes);
        }

        for (var i = 0;  i < stringChars.Length; i++) {
            stringChars[i] = chars[randomBytes[i] % chars.Length];
        }

        var final = new string(stringChars);

        return final;
    }
}
