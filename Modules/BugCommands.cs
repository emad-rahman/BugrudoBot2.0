
using System;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugrudoBot.Modules
{
    public class BotCommands : BaseCommands
    {
        private readonly IConfigurationRoot _config;

        public BotCommands(IServiceProvider services)
        {
            _config = services.GetRequiredService<IConfigurationRoot>();
        }

        [Command("bug")]
        [Summary("Shows a list of the 10 most recent bugs")]
        public async Task GifCommand(params String[] stringArray) {
            // await Log(msg: new Discord.LogMessage(message: "Starting GifCommand function", severity: Discord.LogSeverity.Info, source: "GifCommand"));

            // await Context.Channel.TriggerTypingAsync();

            // var query = string.Join(" ", stringArray);
            // await Log(msg: new Discord.LogMessage(message: $"User typed: [{query}]", severity: Discord.LogSeverity.Info, source: "GifCommand"));

            // query = $"spongebob {query}".Trim();

            // await TenorSearch(query);

            await Context.Channel.SendMessageAsync($"Hi, {Context.User.Username} :)");
        }
    }
}