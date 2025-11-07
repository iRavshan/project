namespace UserService.Infrastructure.Middlewares;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public double ExpireHours { get; set; }
}