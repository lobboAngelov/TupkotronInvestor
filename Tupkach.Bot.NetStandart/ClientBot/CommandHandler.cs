using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Ninject;
using Tupkach.Bot.NetStandart.ClientBot.Context;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;

namespace Tupkach.Bot.NetStandart.ClientBot
{
    public class CommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IKernel _lifetimeScope;

        public CommandHandler(DiscordSocketClient client, CommandService service, IKernel lifetimeScope)
        {
            _client = client;
            _service = service;
            _lifetimeScope = lifetimeScope;
            _service.AddModulesAsync(Assembly.GetExecutingAssembly()).Wait();
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage message)
        {
            if (message is SocketUserMessage userMessage)
            {
                var context = new TupkachCommandContext(_client, userMessage, _lifetimeScope);
                var argPos = 0;
                if (userMessage.HasStringPrefix("kur ", ref argPos, StringComparison.InvariantCultureIgnoreCase))
                {
                    var result = await _service.ExecuteAsync(context, argPos);

                    if (!result.IsSuccess)
                    {
                        await context.Channel.SendMessageAsync(result.ErrorReason);
                    }
                }
            }
        }

        private void SendAll()
        {

        }

        public void Execute()
        {

        }
    }
}
