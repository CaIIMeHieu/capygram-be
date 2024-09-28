using Cassandra;

namespace capygram.Newsfeed.Domain.Data
{
    public interface INewsfeedContext
    {
        public Cassandra.ISession context { get; set; }
    }
}
