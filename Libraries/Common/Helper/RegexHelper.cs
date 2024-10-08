using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common.Helper;

public class RegexHelper
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsValidPassword(string password)
    {
        return true;
    }

    public static string ConvertToSnakeCase(string value)
    {
        var snakeCase = Regex.Replace(value, "([a-z])([A-Z])", "$1_$2").ToLower();
        return snakeCase;
    }
}
