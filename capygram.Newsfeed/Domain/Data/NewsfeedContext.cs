using capygram.Newsfeed.DependencyInjection.Options;
using Cassandra;
using Microsoft.Extensions.Options;

namespace capygram.Newsfeed.Domain.Data
{
    public class NewsfeedContext : INewsfeedContext
    {
        public NewsfeedContext( IOptionsMonitor<NewsfeedDBSetting> optionsMonitor ) { 
            var setting = optionsMonitor.CurrentValue;
            var cluster = Cluster.Builder()
            .AddContactPoint(setting.ConnectionString) 
            .Build();
            var session = cluster.Connect(setting.KeyspaceName);
            this.context = session;
        }

        public Cassandra.ISession context { get; set; }
    }
}
