namespace Services.Accounts.Core.Settings;

public class SessionTokenSettings
{
    public int AccessTokenTTL { get; set; }
    public int RefreshTokenTTL { get; set; }
}