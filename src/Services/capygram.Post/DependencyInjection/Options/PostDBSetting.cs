namespace capygram.Post.DependencyInjection.Options
{
    public class PostDBSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PostCollectionName { get; set; } = null!;
    }
}
