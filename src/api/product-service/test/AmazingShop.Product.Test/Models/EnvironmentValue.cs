namespace AmazingShop.Product.Test.Model
{
    public class EnvironmentValue
    {
        public EnvironmentValue()
        {

        }
        public EnvironmentValue(string key, string value, bool enable = true)
        {
            Key = key;
            Value = value;
            Enabled = enable;
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }
    }
}