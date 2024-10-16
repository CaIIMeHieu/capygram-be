namespace capygram.Newsfeed.DependencyInjection.Options
{
    public class NewsfeedDBSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string KeyspaceName { get; set; } = null!;
        public string NewsfeedTableName { get; set; } = null!;
    }
}
