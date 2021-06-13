using System;
using System.Threading.Tasks;
using BugrudoBot.Models;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BugrudoBot.Modules
{
    public class RestoreCommands : BaseCommands
    {
        private readonly ApplicationDbContext _db;
        
        public RestoreCommands(IServiceProvider services)
        {
            _db = services.GetRequiredService<ApplicationDbContext>();
        }

        [Command("restorebug")]
        public async Task RestoreCommand(int bugId)
        {
            await Log(msg: new Discord.LogMessage(message: "Starting Restore command", severity: Discord.LogSeverity.Info, source: "Restore"));

            var bug = _db.Bugs.SingleOrDefaultAsync(b => b.Id == bugId);
            if (bug.Result == null)
            {
                await Log(msg: new Discord.LogMessage(message: "Didn't find the bug", severity: Discord.LogSeverity.Info, source: "Restore"));
                await Context.Channel.SendMessageAsync("Sorry, I couldn't find that bug or it doesn't exist :(");
                return;
            }

            await Log(msg: new Discord.LogMessage(message: "Restoring bug", severity: Discord.LogSeverity.Info, source: "Restore"));
            bug.Result.IsDeleted = false;
            await _db.SaveChangesAsync();
            await Context.Channel.SendMessageAsync($"Successfully restored bug {bug.Result.Id}. ```Bug: {bug.Result.Text}```");
            
            await Log(msg: new Discord.LogMessage(message: "Exiting Restore command", severity: Discord.LogSeverity.Info, source: "Restore"));
        }
    }
}