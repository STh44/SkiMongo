namespace Ski_ServiceNoSQL.Services
{
    public interface ITokenService
    {
        string CreateToken(string username);
    }
}
