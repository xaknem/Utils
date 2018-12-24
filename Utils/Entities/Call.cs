using System.Diagnostics.CodeAnalysis;

namespace Utils.Entities
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Call
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Queue { get; set; }
        public string Operator { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; } = false;
        public string Comment { get; set; }
        public string Sla { get; set; }
    }
}