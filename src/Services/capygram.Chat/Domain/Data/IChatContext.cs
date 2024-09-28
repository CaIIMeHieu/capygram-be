namespace capygram.Chat.Domain.Data
{
    public interface IChatContext
    {
        public Cassandra.ISession context { get; set; }
    }
}
