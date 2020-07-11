using System.Collections.Generic;

namespace AmazingShop.Product.Test.Model
{
    public class PostmanRequest
    {
        public PostmanUrl Url { get; set; }
        public string Method { get; set; }
        public List<PostmanHeaderData> Header { get; set; }
        public PostmanBody Body { get; set; }
    }
    public class PostmanHeaderData
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Disabled { get; set; }
    }
}