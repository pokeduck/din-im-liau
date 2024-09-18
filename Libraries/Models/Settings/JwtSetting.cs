namespace Models.Settings;
#nullable disable
public class JwtSetting
{

    public string Issuer { get; set; }
    public string Key { get; set; }
    public int RefreshTokenExpireSeconds { get; set; }
    public int AccessTokenExpireSeconds { get; set; }

    public int EmailTokenExpireSeconds { get; set; }
}
