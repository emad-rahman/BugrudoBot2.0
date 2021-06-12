using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BugrudoBot.Modules
{
    public class HelpCommands : BaseCommands
    {
        [Command("Help")]
        public async Task HelpCommand() {
            var embedBuilder = new EmbedBuilder();

            embedBuilder.WithTitle("BugrudoBot2.0 Help");
            embedBuilder.WithFooter("I am using a real database and am written in dotnet core :)");
            embedBuilder.AddField("!bug This is a test bug", "Logs a bug with the text \"This is a test bug\"", false);
            embedBuilder.AddField("!bugs", "Shows a list of all bugs", false);

            await Context.Channel.SendMessageAsync("", false, embedBuilder.Build());
        }
    }
}