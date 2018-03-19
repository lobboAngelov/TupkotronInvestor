using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Tupkach.Bot.NetStandart.ClientBot.Context;

namespace Tupkach.Bot.NetStandart.ClientBot.Modules
{
    public class DefaultModule : ModuleBase<TupkachCommandContext>
    {
        [Command("Vesti")]
        public async Task Vesti()
        {
            await Context.Channel.SendMessageAsync("www.vesti.bg");
        }

        [Command("BigIron")]
        public async Task BigIron()
        {
            var targetDate = new DateTime(2018,8,5);
            var daysLeft = (targetDate - DateTime.Now).Days;
            await Context.Channel.SendMessageAsync($"{daysLeft} days until B I G   I R O N");
        }

        [Command("Decide")]
        public async Task Decide(params string[] args)
        {
            var random = new Random();
            var randomInt = random.Next(0, args.Length);
            await Context.Channel.SendMessageAsync(args[randomInt]);
        }

        [Command("Istini")]
        public async Task Truth()
        {
            await Context.Channel.SendMessageAsync(
                "https://cdn.discordapp.com/attachments/304919616780763136/418538952325333022/e7c229d8e532aa5340b23e4c605a7b8c--black-kids-funny-memes.jpg");
        }

        [Command("Smazan")]
        public async Task Smazan(string str)
        {
            var person = Context.Message.MentionedUsers.First().ToString().Split('#').Last();

            switch (person)
            {
                case "3211":
                    await Context.Channel.SendFileAsync("Content/kalo.jpg");
                    break;
                case "4359":
                    await Context.Channel.SendFileAsync("Content/lobo.png");
                    break;
                case "8084":
                    await Context.Channel.SendFileAsync("Content/tsekov.png");
                    break;
                default:
                    await Context.Channel.SendFileAsync("Content/Smazan.png");
                    break;
            }
        }

        [Command("Senor")]
        public async Task Hilter()
        {
            await Context.Channel.SendMessageAsync("Sieg Hilter!");
            await Context.Channel.SendFileAsync("Content/senor.png");
        }

        [Command("komandi")]
        public async Task Commands()
        {
            var type = typeof(ModuleBase<TupkachCommandContext>);
            var modules = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => type.IsAssignableFrom(x));

            var commands = new List<CommandAttribute>();
            foreach (var module in modules)
            {
                commands.AddRange(module.GetMethods()
                    .Select(x => x.GetCustomAttribute<CommandAttribute>())
                    .Where(x => x != null));
            }
            foreach (var commandAttribute in commands)
            {
                await Context.Channel.SendMessageAsync(commandAttribute.Text.ToLower());
            }
        }

        [Command("Kill")]
        public async Task Die()
        {
            await Context.Channel.SendMessageAsync("Nqma poveche umirane");
            var task = Task.Run(async () =>
            {
                await Task.Delay(15000);
                await Context.Channel.SendFileAsync("Smeshnik");
            });
            await task;
            Environment.Exit(-1);
        }
    }
}
