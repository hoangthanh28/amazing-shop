using MyShop.Service.Abstraction;

namespace MyShop.Service
{
    public class UserContext : IUserContext
    {
        private string _accessToken;
        private string _refreshToken;
        public string GetAccessToken() => _accessToken;

        public string GetRefreshToken() => _refreshToken;

        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }

        public void SetRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
        }
    }
}