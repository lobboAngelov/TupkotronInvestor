using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;
using Tupkach.Bot.NetStandart.Services.Interfaces;

namespace Tupkach.Bot.NetStandart.ClientBot
{
    public class ClientBot : IClientBot
    {     
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly IConfigurationProvider _configurationProvider;

        private SocketGuild _mainServer;
        private SocketTextChannel _logChannel;

        public ClientBot(DiscordSocketClient discordSocketClient, IConfigurationProvider configurationProvider)
        {
            _discordSocketClient = discordSocketClient;
            _configurationProvider = configurationProvider;
            _discordSocketClient.Connected += OnConnected;
            _discordSocketClient.Ready += OnReady;

            _discordSocketClient.UserVoiceStateUpdated += OnVoiceChange;
        }

        private async Task OnVoiceChange(SocketUser socketUser, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            var symbol = oldVoiceState.ToString() == "Unknown" ? ":heavy_plus_sign:" : ":heavy_minus_sign:";

            var message = $"**{socketUser.Username}** {symbol} __**Voice**__";
            await LogAsync(message);
        }

        private async Task OnReady()
        {
            Console.WriteLine("Channel:");
            foreach (var socketGuild in _discordSocketClient.Guilds)
            {
                Console.WriteLine(socketGuild.Id);
                Console.WriteLine(socketGuild.Name);
                foreach (var socketUser in socketGuild.Users)
                {
                    Console.WriteLine(socketUser.Username);
                }
            }

            _mainServer = _discordSocketClient.Guilds.FirstOrDefault(x => x.Name == _configurationProvider.GetConfigurationParameter(StaticData.ServerNameConfigurationKey));
            if (_mainServer == null)
            {
                Console.WriteLine("Main server not found, Logging will not be availale");
            }
            _logChannel = _mainServer.TextChannels.First(x => x.Name == _configurationProvider.GetConfigurationParameter(StaticData.LogChannelConfigurationKey));

            await _discordSocketClient.SetGameAsync("Minecraft 1.7.10");
        }

        private async Task OnConnected()
        {
            Console.WriteLine("Connected");
            await Task.FromResult((Task)null);
        }

        public async Task StartAsync() => await _discordSocketClient.StartAsync();

        public async Task LogAsync(string message)
        {
            Console.WriteLine(message);
            if (_logChannel != null)
            {
                await _logChannel?.SendMessageAsync(message);
            }           
        }
    }
}
