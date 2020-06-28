namespace IdentityServer.Model
{
    public class ExternalProviderConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}