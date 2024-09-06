namespace Common.Extensions;




public static class DateTimeExtensions
{
    public enum TimeUnit
    {
        Seconds,
        Milliseconds
    }
    public static long ToTimestamp(this DateTime source, TimeUnit timeUnit = TimeUnit.Milliseconds)
    => timeUnit switch
    {
        TimeUnit.Seconds => ((DateTimeOffset)source).ToUnixTimeSeconds(),
        TimeUnit.Milliseconds => ((DateTimeOffset)source).ToUnixTimeMilliseconds(),
        _ => throw new NotImplementedException("不支援的轉換單位"),
    };

    public static string ToUnixTimeSecondsString(this DateTime source)
    {
        return (new DateTimeOffset(source)).ToUnixTimeSeconds().ToString();
    }

    public static long ToUnixTimeSeconds(this DateTime source)
    {
        return (new DateTimeOffset(source)).ToUnixTimeSeconds();
    }
}
