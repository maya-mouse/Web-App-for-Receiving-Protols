namespace NetworkingReferenceBasics.Models
{
    public class AdapterModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DnsSuffix { get; set; }
        public bool IsDnsEnabled { get; set; }
        public bool IsDynamicDnsEnabled { get; set; }

        public string InterfaceType { get; set; }
        public string PhysicalAddress { get; set; }
        public bool SuppotingIp4 { get; set; }
        public bool SuppotingIp6 { get; set; }

        public bool isRecieveOnly { get; set; }
        public bool Multicast { get; set; }

    }
}
