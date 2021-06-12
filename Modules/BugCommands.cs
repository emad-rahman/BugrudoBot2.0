
using System;
using System.Threading.Tasks;
using BugrudoBot.Models;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Discord;
using System.Linq;

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
        public async Task BugCommand([Remainder] string bugText)
        {
            await Log(msg: new Discord.LogMessage(message: "Starting Bug command", severity: Discord.LogSeverity.Info, source: "bug"));

            var reportedBy = Context.User.Username;
            var reportedOn = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

            await Log(msg: new Discord.LogMessage(message: "Creating bug object", severity: Discord.LogSeverity.Info, source: "bug"));
            var bug = new Bug
            {
                ReportedOn = DateTime.Now,
                ReportedBy = Context.User.Username,
                Text = bugText
            };

            await Log(msg: new Discord.LogMessage(message: "Adding bug object to db", severity: Discord.LogSeverity.Info, source: "bug"));
            await _db.Bugs.AddAsync(bug);
            await _db.SaveChangesAsync();

            await Context.Channel.SendMessageAsync($"Added bug: {bugText}");

            await Log(msg: new Discord.LogMessage(message: "Exiting Bug command", severity: Discord.LogSeverity.Info, source: "bug"));
        }

        [Command("bugs")]
        [Summary("Shows a list of the 10 most recent bugs")]
        public async Task ListBugsCommand()
        {
            await Log(msg: new Discord.LogMessage(message: "Starting Bugs Command", severity: Discord.LogSeverity.Info, source: "bugs"));
            var embedBuilder = new EmbedBuilder();

            await Log(msg: new Discord.LogMessage(message: "Accessing db", severity: Discord.LogSeverity.Info, source: "bugs"));
            var bugs = await _db.Bugs
                .AsQueryable()
                .OrderBy(b => b.ReportedOn)
                .ToListAsync();

            await Log(msg: new Discord.LogMessage(message: "Merging bugs into log", severity: Discord.LogSeverity.Info, source: "bugs"));
            string bugsAsString = "";
            if (bugs.Count > 0)
            {
                foreach (var bug in bugs)
                {
                    bugsAsString += $"```{bug.ReportedOn.ToString("yyyy-MM-dd hh:mm tt")} | Reported by: {bug.ReportedBy}``` {bug.Text}\n";
                }
            }

            embedBuilder.WithTitle("Perudobot bugs");
            embedBuilder.WithDescription(bugsAsString);

            await Log(msg: new Discord.LogMessage(message: "Sending list to channel", severity: Discord.LogSeverity.Info, source: "bugs"));
            await Context.Channel.SendMessageAsync("", false, embedBuilder.Build());

            await Log(msg: new Discord.LogMessage(message: "Exiting Bugs command", severity: Discord.LogSeverity.Info, source: "bugs"));
        }
    }
}