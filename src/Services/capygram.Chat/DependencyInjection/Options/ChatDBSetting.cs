namespace capygram.Chat.DependencyInjection.Options
{
    public class ChatDBSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string KeyspaceName { get; set; } = null!;
        public string NewsfeedTableName { get; set; } = null!;
    }
}
