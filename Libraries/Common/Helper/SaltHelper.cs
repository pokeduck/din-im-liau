namespace Common.Helper;

public class SaltHelper
{
    public static string GenerateN()
    {
        return Guid.NewGuid().ToString("N");
    }
}
