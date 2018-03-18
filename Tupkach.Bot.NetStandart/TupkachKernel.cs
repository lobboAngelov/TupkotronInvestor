using Discord.Commands;
using Discord.WebSocket;
using Ninject.Modules;
using Ninject;

using Tupkach.Bot.NetStandart.ClientBot;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;
using Tupkach.Bot.NetStandart.Services.Interfaces;
using Tupkach.Bot.NetStandart.Services;

namespace Tupkach.Bot.NetStandart
{
    public class TupkachKernel : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();
            Bind<ISocketClientFactory>().To<SocketClientFactory>().InSingletonScope();

            Bind<DiscordSocketClient>().ToMethod(ctx =>
            {
                return ctx.Kernel.Get<ISocketClientFactory>().CreateSocketClientAsync().Result;
            }).InSingletonScope();

            Bind<CommandService>().ToSelf().InSingletonScope();
            Bind<IClientBot,ILogger>().To<ClientBot.ClientBot>().InSingletonScope();
            Bind<ICommandHandler>().To<CommandHandler>().InSingletonScope();
            Bind<IScheduler>().To<Scheduler>().InSingletonScope();
        }
    }
}
