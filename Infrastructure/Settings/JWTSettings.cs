namespace Infrastructure.Settings;

public class JWTSettings
{
	public string Key { get; set; } = string.Empty;
	public string Issuer { get; set; } = string.Empty;
	public TimeSpan AccessLifetime { get; set; }

}
