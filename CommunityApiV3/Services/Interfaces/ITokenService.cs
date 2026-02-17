namespace CommunityApiV3.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(int userId, string username);
    }
}
