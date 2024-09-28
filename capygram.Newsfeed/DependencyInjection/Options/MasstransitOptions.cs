namespace capygram.Newsfeed.DependencyInjection.Options
{
    public class MasstransitOptions
    {
        public string Host { get; set; }
        public string VHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExchangeMediaName { get; set; }
        public string ExchangeType { get; set; }
        public string NewsfeedQueueName { get; set; }
    }
}
