namespace NetworkingReferenceBasics.Models
{
    public class TcpModel
    {
        public int Id { get; set; }
        public string SourcePort { get; set; }
        public string DestinationPort { get; set; }
        public string Flags { get; set; }
    }
}
