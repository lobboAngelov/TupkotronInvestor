using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;

namespace Tupkach.Bot.NetStandart.ClientBot
{
    public class ClientBot : IClientBot
    {
        private const ulong TupkachiChannelId = 304919616780763136;

        private readonly DiscordSocketClient _discordSocketClient;
        
        public ClientBot(DiscordSocketClient discordSocketClient)
        {
            _discordSocketClient = discordSocketClient;
            
            _discordSocketClient.Connected += OnConnected;
            _discordSocketClient.Ready += OnReady;

            _discordSocketClient.UserVoiceStateUpdated += OnVoiceChange;
        }

        private async Task OnVoiceChange(SocketUser socketUser, SocketVoiceState oldVoiceState, SocketVoiceState newVoiceState)
        {
            var socketGuild = _discordSocketClient.Guilds.First(x => x.Id == TupkachiChannelId);
            var socketTextChannel = socketGuild.TextChannels
                .First(x => x.Name == "voice_log");

            var symbol = oldVoiceState.ToString() == "Unknown" ? ":heavy_plus_sign:" : ":heavy_minus_sign:";

            var message = $"**{socketUser.Username}** {symbol} __**Voice**__";
            await socketTextChannel.SendMessageAsync(message);
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

            var channelId = StaticData.Mode == "test" ? 340214016813301760 : TupkachiChannelId;

            await _discordSocketClient.SetGameAsync("Minecraft 1.7.10");
        }

        private async Task OnConnected()
        {
            Console.WriteLine("Connected");
            await Task.FromResult((Task)null);
        }

        public async Task StartAsync() => await _discordSocketClient.StartAsync();

        public void Log(string message)
        {
            Console.WriteLine(message);

        }
    }
}
