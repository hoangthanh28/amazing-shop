namespace MyShop.Service.Abstraction
{
    public interface IUserContext
    {
        string GetAccessToken();
        string GetRefreshToken();
        void SetAccessToken(string token);
        void SetRefreshToken(string refreshToken);
    }
}