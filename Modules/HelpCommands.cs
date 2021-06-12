using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BugrudoBot.Modules
{
    public class HelpCommands : BaseCommands
    {
        [Command("Help")]
        public async Task HelpCommand() {
            var embedBuilder = new EmbedBuilder()
                .WithTitle("BugrudoBot2.0 Help")
                .WithFooter("I am using a real database and am written in dotnet core :)")
                .AddField("!bug This is a test bug", "Logs a bug with the text \"This is a test bug\"", false)
                .AddField("!bugs", "Shows a list of all bugs", false);

            await Context.Channel.SendMessageAsync("", false, embedBuilder.Build());
        }
    }
}