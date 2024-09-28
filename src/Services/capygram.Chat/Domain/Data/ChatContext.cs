
using capygram.Chat.DependencyInjection.Options;
using Cassandra;
using Microsoft.Extensions.Options;

namespace capygram.Chat.Domain.Data
{
    public class ChatContext : IChatContext
    {
        public ChatContext( IOptionsMonitor<ChatDBSetting> optionsMonitor ) {
            var setting = optionsMonitor.CurrentValue;
            var cluster = Cluster.Builder()
            .AddContactPoint(setting.ConnectionString)
            .Build();
            var _context = cluster.Connect(setting.KeyspaceName);
            this.context = _context;
        }
        public Cassandra.ISession context { get;set; }
    }
}
