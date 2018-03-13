using Discord.Commands;
using Discord.WebSocket;
using Ninject;

namespace Tupkach.Bot.NetStandart.ClientBot.Context
{
    public class TupkachCommandContext : SocketCommandContext
    {
        public IKernel LifetimeScope { get; }
        public TupkachCommandContext(DiscordSocketClient client, SocketUserMessage msg, IKernel lifetimeScope) : base(client, msg)
        {
            LifetimeScope = lifetimeScope;
        }
    }
}