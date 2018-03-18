using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using Ninject;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;
using Tupkach.Bot.NetStandart.ClientBot.Modules;

namespace Tupkach.Bot.NetStandart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().MainAsync(args).GetAwaiter().GetResult();
        }   
        
        public async Task MainAsync(string[] args)
        {
            using (IKernel kernel = new StandardKernel(new TupkachKernel()))
            {
                StaticData.LifetimeScope = kernel;
                kernel.Load(Assembly.GetExecutingAssembly());
                var bot = kernel.Get<IClientBot>();
                var handler = kernel.Get<ICommandHandler>();
                handler.Execute();
                await bot.StartAsync();
                await bot.LogAsync("Kur za neshtastieto");
                await Task.Delay(-1);
            }
        }
    }
}
