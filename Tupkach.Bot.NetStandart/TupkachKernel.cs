using Discord.Commands;
using Discord.WebSocket;
using Ninject.Modules;
using Ninject;

using Tupkach.Bot.NetStandart.ClientBot;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;
using Tupkach.Bot.NetStandart.Services.Interfaces;
using Tupkach.Bot.NetStandart.Services;
using Tupkach.Bot.Infrastructure;

namespace Tupkach.Bot.NetStandart
{
    internal class TupkachKernel : NinjectModule
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

            Bind<TupkachDbContext>().To<TupkachDbContext>();
            Bind<IRepository>().To<TupkachRepository>();
        }
    }
}
