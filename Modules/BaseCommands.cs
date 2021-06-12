using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BugrudoBot.Modules
{
    public class BaseCommands : ModuleBase
    {
        protected Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}