using System;
using System.Threading.Tasks;
using BugrudoBot.Models;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BugrudoBot.Modules
{
    public class DeleteCommands : BaseCommands
    {
        private readonly ApplicationDbContext _db;

        public DeleteCommands(IServiceProvider services)
        {
            _db = services.GetRequiredService<ApplicationDbContext>();
        }

        [Command("deletebug")]
        public async Task DeleteCommand(int bugId)
        {
            await Log(msg: new Discord.LogMessage(message: "Starting Delete command", severity: Discord.LogSeverity.Info, source: "Delete"));

            var bug = _db.Bugs.SingleOrDefaultAsync(b => b.Id == bugId);
            if (bug.Result == null)
            {
                await Log(msg: new Discord.LogMessage(message: "Didn't find the bug", severity: Discord.LogSeverity.Info, source: "Delete"));
                await Context.Channel.SendMessageAsync("Sorry, I couldn't find that bug or it doesn't exist :(");
                return;
            }

            await Log(msg: new Discord.LogMessage(message: "Deleting bug", severity: Discord.LogSeverity.Info, source: "Delete"));
            bug.Result.IsDeleted = true;
            await _db.SaveChangesAsync();
            await Context.Channel.SendMessageAsync($"Successfully deleted bug {bug.Result.Id}. ```Bug: {bug.Result.Text}```");

            await Log(msg: new Discord.LogMessage(message: "Exiting Delete command", severity: Discord.LogSeverity.Info, source: "Delete"));
        }
    }
}