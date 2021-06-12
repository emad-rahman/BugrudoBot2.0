using System.Threading.Tasks;

namespace BugrudoBot
{
    public class Program
    {
        public static Task Main(string[] args)
            =>  Startup.RunAsync(args);
    }
}
