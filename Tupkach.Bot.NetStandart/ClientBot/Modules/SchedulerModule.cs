using System;
using System.Threading.Tasks;
using Discord.Commands;
using Ninject;
using Tupkach.Bot.NetStandart.ClientBot.Context;
using Tupkach.Bot.NetStandart.ClientBot.Interfaces;

namespace Tupkach.Bot.NetStandart.ClientBot.Modules
{
    public class SchedulerModule : ModuleBase<TupkachCommandContext>
    {
        [Command("reminder")]
        public async Task Schedule(int hour, int minute,params string[] message)
        {
            var scheduler = Context.LifetimeScope.Get<IScheduler>();
            var socketMessageChannel = Context.Channel;
            scheduler.Schedule(new DateTime(2018,3,1,hour, minute,0), "generic", state =>
            {
                socketMessageChannel.SendMessageAsync(string.Join(" ",message)).Wait();
            } );
            await Task.FromResult(null as Task);
        }

        [Command("reminder list")]
        public async Task List()
        {
            var scheduler = Context.LifetimeScope.Get<IScheduler>();
            var message = scheduler.PrintTimers();
            await Context.Channel.SendMessageAsync(message);
        } 
    }
}
