using System;

namespace BugrudoBot.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ReportedBy { get; set; }
        public DateTime ReportedOn { get; set; }
    }
}