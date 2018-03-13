using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Ninject.Modules;
using Tupkach.Bot.NetStandart.ClientBot;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;

namespace Tupkach.Bot.NetStandart
{
    class TupkachKernel : NinjectModule
    {
        public override void Load()
        {
            Bind<DiscordSocketClient>().ToMethod(ctx =>
            {
                Console.WriteLine("Creating socket");
                var client = new DiscordSocketClient();

                const string testBotToken = "NDIwNjE5ODAyMTI2NzEyODMy.DYBUSA.16bIIsU4TT6zD9uoBVP1H2H_dO0";
                const string releaseBotToken = "MzcxOTU5NTQ4Nzk1NDIwNjg0.DYBinQ.lrx7bzlHiPPvxDkvoLFV9W2rfdM";

                var token = StaticData.Mode == "test" ? testBotToken : releaseBotToken;
                client.LoginAsync(TokenType.Bot, token).Wait();

                return client;
            }).InSingletonScope();

            Bind<CommandService>().ToSelf().InSingletonScope();
            Bind<IClientBot,ILogger>().To<ClientBot.ClientBot>().InSingletonScope();
            Bind<ICommandHandler>().To<CommandHandler>().InSingletonScope();
            Bind<IScheduler>().To<Scheduler>().InSingletonScope();
        }
    }
}
