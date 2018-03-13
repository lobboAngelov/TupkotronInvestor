using System.Reflection;
using System.Threading.Tasks;
using Ninject;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;

namespace Tupkach.Bot.NetStandart
{
    public class Program
    {
        public static void Main(string[] args) =>
            new Program().MainAsync(args).GetAwaiter().GetResult();
        
        public async Task MainAsync(string[] args)
        {
            using (IKernel kernel = new StandardKernel(new TupkachKernel()))
            {
                kernel.Load(Assembly.GetExecutingAssembly());
                var bot = kernel.Get<IClientBot>();
                var handler = kernel.Get<ICommandHandler>();
                handler.Execute();
                await bot.StartAsync();
                
                await Task.Delay(-1);
            }
        }
    }
}
