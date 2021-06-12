
using System;
using System.Threading.Tasks;
using BugrudoBot.Models;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BugrudoBot.Modules
{
    public class BotCommands : BaseCommands
    {
        private readonly ApplicationDbContext _db;

        public BotCommands(IServiceProvider services)
        {
            _db = services.GetRequiredService<ApplicationDbContext>();
        }


        [Command("bug")]
        [Summary("Shows a list of the 10 most recent bugs")]
        public async Task GifCommand([Remainder] string str) {
            // await Log(msg: new Discord.LogMessage(message: "Starting GifCommand function", severity: Discord.LogSeverity.Info, source: "GifCommand"));

            // await Context.Channel.TriggerTypingAsync();

            // var query = string.Join(" ", stringArray);
            // await Log(msg: new Discord.LogMessage(message: $"User typed: [{query}]", severity: Discord.LogSeverity.Info, source: "GifCommand"));

            // query = $"spongebob {query}".Trim();

            // await TenorSearch(query);
            
            await Log(msg: new Discord.LogMessage(message: str, severity: Discord.LogSeverity.Info, source: "bug"));

            var count = await _db.Bugs.CountAsync();

            await Context.Channel.SendMessageAsync($"There are {count} items in the Bugs table");
            // await ReplyAsync(str);
        }

        	// [Command("say")]
            // [Summary("Echoes a message.")]
            // public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            //     => ReplyAsync(echo);
    }
}