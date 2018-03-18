using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Tupkach.Bot.NetStandart.Services.Interfaces
{
    public interface ISocketClientFactory
    {
        Task<DiscordSocketClient> CreateSocketClientAsync();
    }

    public class SocketClientFactory : ISocketClientFactory
    {
        private readonly IConfigurationProvider _configurationProvider;

        public SocketClientFactory(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }


        public async Task<DiscordSocketClient> CreateSocketClientAsync()
        {
            System.Console.WriteLine("Creating Socket Client");
            var client = new DiscordSocketClient();
            var token = _configurationProvider.GetConfigurationParameter(StaticData.TokenTypeConfigurationKey);
            await client.LoginAsync(TokenType.Bot, token);

            return client;
        }
    }
}
