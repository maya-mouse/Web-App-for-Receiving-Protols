using Microsoft.EntityFrameworkCore;
using NetworkingReferenceBasics.Models;
namespace NetworkingReferenceBasics.Data
{
    public class NetworkingContext : DbContext
    {
        public NetworkingContext(DbContextOptions<NetworkingContext> options) : base(options) { }

        public DbSet<AdapterModel> adapters { get; set; }
        public DbSet<EthernetModel> ethernetProtocols { get; set; }
        public DbSet<IpModel> ipProtocols { get; set; }
        public DbSet<TcpModel>  tcpProtocols { get; set; }
        public DbSet<UdpModel> udpProtocols { get; set; }
    }
}
