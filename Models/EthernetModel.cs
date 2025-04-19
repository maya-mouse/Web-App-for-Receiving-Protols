namespace NetworkingReferenceBasics.Models
{
    public class EthernetModel
    {
        public int Id { get; set; }
        public string SourceHardwareAddress { get; set; }
        public string DestinationHardwareAddress { get; set; }
        public string Type { get; set; }
    }
}
