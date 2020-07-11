namespace AmazingShop.Product.Test.Model
{
    public class PostmanBody
    {
        public string Mode { get; set; }
        public string Raw { get; set; }
        public PostmanFormBody[] Urlencoded { get; set; }
        public string[] Path { get; set; }
    }
    public class PostmanFormBody
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}