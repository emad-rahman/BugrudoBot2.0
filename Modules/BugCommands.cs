
using System;
using System.Threading.Tasks;
using BugrudoBot.Models;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Discord;
using System.Linq;
using BugrudoBot.Helpers;

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

            var reportedBy = Context.Guild.GetUserAsync(Context.User.Id).Result.Nickname ?? Context.User.Username;
            var reportedOn = DateTime.Now.ToString("yyyy-MM-dd hh:mm");

            await Log(msg: new Discord.LogMessage(message: "Creating bug object", severity: Discord.LogSeverity.Info, source: "bug"));
            var bug = new Bug
            {
                ReportedOn = DateTime.Now,
                ReportedBy = reportedBy,
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
                .OrderByDescending(b => b.ReportedOn)
                .ToListAsync();

            await Log(msg: new Discord.LogMessage(message: "Merging bugs into log", severity: Discord.LogSeverity.Info, source: "bugs"));
            string bugsAsString = "";
            if (bugs.Count > 0)
            {
                foreach (var bug in bugs)
                {
                    var str = $"```{bug.ReportedOn.ToString("yyyy-MM-dd hh:mm tt")} | Reported by: {bug.ReportedBy}``` {bug.Text.StripSpecialCharacters()}\n";

                    if (bugsAsString.Length + str.Length >= EmbedBuilder.MaxDescriptionLength)
                    {
                        break;
                    }

                    bugsAsString += str;
                }
            }

            embedBuilder
                .WithTitle("Page 1")
                .WithDescription(bugsAsString)
                .WithFooter("Page turning is currently disabled due to technical difficulties");

            await Log(msg: new Discord.LogMessage(message: "Sending list to channel", severity: Discord.LogSeverity.Info, source: "bugs"));
            var message = await Context.Channel.SendMessageAsync("", false, embedBuilder.Build());

            await message.AddReactionAsync(new Emoji("➡️"));

            await Log(msg: new Discord.LogMessage(message: "Exiting Bugs command", severity: Discord.LogSeverity.Info, source: "bugs"));
        }

        // [Command("➡️")]
        // public async Task NextPage()
        // {
        //     await Log(msg: new Discord.LogMessage(message: GetCurrentPage(Context.Message).ToString(), severity: Discord.LogSeverity.Info, source: "bugs"));
        //     await Context.Channel.SendMessageAsync(GetCurrentPage(Context.Message).ToString());
        // }

        // private static int GetCurrentPage(IUserMessage message)
        // {
        //     var currentPage = 1;
        //     if (message.Embeds.First().Title.Contains("Page"))
        //     {
        //         var pageText = message.Embeds.First().Title.Split("Page")[1];
        //         currentPage = int.Parse(pageText);
        //     }

        //     return currentPage;
        // }
    }
}