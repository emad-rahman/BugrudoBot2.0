namespace BugrudoBot.Helpers
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionString() => $"Data Source={absolutePath}/bugrudo.db";

        // Mac
        private static string absolutePath = "/Users/emadrahman/Projects/discordBots/BugrudoBot2.0";
        // Pi
        // private static string absolutePath = "/Users/emadrahman/Projects/discordBots/BugrudoBot2.0";

        
    }
}