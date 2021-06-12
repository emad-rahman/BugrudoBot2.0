namespace BugrudoBot.Helpers
{
    public static class StringHelper
    {
        public static string StripSpecialCharacters(this string str)
        {
            return str
                .Replace("`", "")
                .Replace("_", "")
                .Replace(">", "")
                .Replace("*", "");
        }
    }
}