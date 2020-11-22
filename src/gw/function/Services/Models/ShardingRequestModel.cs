namespace Function.Service.Model
{
    public class ShardingRequestModel
    {
        public string ServiceName { get; set; }
        public string EnvironmentName { get; set; }
        public string DatabaseName { get; set; }
    }
}