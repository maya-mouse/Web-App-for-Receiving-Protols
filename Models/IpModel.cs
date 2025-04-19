namespace NetworkingReferenceBasics.Models
{
    public class IpModel
    {
        public int Id { get; set; }
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string HeaderLenght { get; set; }
        public string TimeToLive { get; set; }

    }
}
