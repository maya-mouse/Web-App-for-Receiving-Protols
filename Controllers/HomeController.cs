using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetworkingReferenceBasics.Data;
using NetworkingReferenceBasics.Models;
using PacketDotNet;
using SharpPcap;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;


namespace NetworkingReferenceBasics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NetworkingContext _context;
        private int defaultcurrentDevice = 4;
        private bool captured = false;
        private string type = "";
        private ManualResetEvent captureEvent = new ManualResetEvent(false);

        public HomeController(ILogger<HomeController> logger, NetworkingContext context)
        {
            _logger = logger;
            _context = context;

            if (_context.adapters.IsNullOrEmpty())
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var adapter in adapters)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();

                    _context.adapters.Add(new AdapterModel
                    {
                        Description = adapter.Description,
                        DnsSuffix = properties.DnsSuffix,
                        IsDnsEnabled = properties.IsDnsEnabled,
                        IsDynamicDnsEnabled = properties.IsDynamicDnsEnabled,
                        InterfaceType = adapter.NetworkInterfaceType.ToString(),
                        PhysicalAddress = adapter.GetPhysicalAddress().ToString(),
                        SuppotingIp4 = adapter.Supports(NetworkInterfaceComponent.IPv4),
                        SuppotingIp6 = adapter.Supports(NetworkInterfaceComponent.IPv6),
                        isRecieveOnly = adapter.IsReceiveOnly,
                        Multicast = adapter.SupportsMulticast
                    });
                    _context.SaveChanges();
                }
            }
        }

        public IActionResult Index()
        {
            return View(_context.adapters);
        }

        private string CapturePacket(string packetType, Action saveAction)
        {
            var devices = CaptureDeviceList.Instance;
            var device = devices[defaultcurrentDevice];
            foreach (var dev in devices)
            {
                if (dev.Description.Contains("Wireless"))
                {
                    device = dev;
                    defaultcurrentDevice = devices.IndexOf(dev);
                }
            }
            type = packetType;
            Console.WriteLine($"Використовується пристрій: {device.Description}\n");
            foreach ( var adapter in devices)
            {
                Console.WriteLine(adapter.Description);
            }
            device.OnPacketArrival += new PacketArrivalEventHandler(OnPacketArrival);
            device.Open(DeviceModes.Promiscuous, 10);
            device.StartCapture();
            bool received = captureEvent.WaitOne(5000); 
            device.StopCapture();
            device.Close();
            captured = false;
            captureEvent.Reset();

            if (!received)
            {
                return "No packets received within 5 seconds.";
            }
            saveAction();
            return null;
        }

        public IActionResult IPPackets(bool value)
        {
            string error = null;
            
            if (value)
            {
                error = CapturePacket("PacketDotNet.IPPacket", () => { });
            }
            if (error != null) ViewBag.Error = error;
            return View(_context.ipProtocols);
        }

        public IActionResult EthernetPackets(bool value)
        {
            string error = null;
           
            if (value)
            {
                error = CapturePacket("PacketDotNet.EthernetPacket", () => { });
            }
            if (error != null) ViewBag.Error = error;
            return View(_context.ethernetProtocols);
        }

        public IActionResult TCPPackets(bool value)
        {
            string error = null;
           
            if (value)
            {
                error = CapturePacket("PacketDotNet.TcpPacket", () => { });
            }
            if (error != null) ViewBag.Error = error;
            return View(_context.tcpProtocols);
        }

        public IActionResult UDPPackets(bool value)
        {
            string error = null;
           
            if (value)
            {
                error = CapturePacket("PacketDotNet.UdpPacket", () => { });
            }
            if (error != null) ViewBag.Error = error;
            return View(_context.udpProtocols);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void OnPacketArrival(object sender, PacketCapture e)
        {
            var rawPacket = e.GetPacket();
            var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            if (packet is null) return;


            if (type == "PacketDotNet.EthernetPacket")
            {
                EthernetPacket ethernetPacket = packet.Extract<EthernetPacket>();
                if (ethernetPacket is null) return;
                EthernetModel ethernetModel = new EthernetModel
                {
                    DestinationHardwareAddress = ethernetPacket.DestinationHardwareAddress.ToString(),
                    SourceHardwareAddress = ethernetPacket.SourceHardwareAddress.ToString(),
                    Type = ethernetPacket.Type.ToString()
                };
                _context.ethernetProtocols.Add(ethernetModel);
                _context.SaveChanges();
            }
            else if (type == "PacketDotNet.IPPacket")
            {
                IPPacket ipPacket = packet.Extract<IPPacket>();
                if (ipPacket is null) return;
                IpModel ipModel = new IpModel
                {
                    SourceAddress = ipPacket.SourceAddress.ToString(),
                    DestinationAddress = ipPacket.DestinationAddress.ToString(),
                    HeaderLenght = ipPacket.HeaderLength.ToString(),
                    TimeToLive = ipPacket.TimeToLive.ToString()
                };
                _context.ipProtocols.Add(ipModel);
                _context.SaveChanges();
            }
            else if (type == "PacketDotNet.TcpPacket")
            {
                TcpPacket tcpPacket = packet.Extract<TcpPacket>();
                if (tcpPacket is null) return;
                TcpModel tcpModel = new TcpModel
                {
                    SourcePort = tcpPacket.SourcePort.ToString(),
                    DestinationPort = tcpPacket.DestinationPort.ToString(),
                    Flags = tcpPacket.Flags.ToString()
                };
                _context.tcpProtocols.Add(tcpModel);
                _context.SaveChanges();
            }
            else if (type == "PacketDotNet.UdpPacket")
            {
                UdpPacket udpPacket = packet.Extract<UdpPacket>();
                if (udpPacket is null) return;
                UdpModel udpModel = new UdpModel
                {
                    SourcePort = udpPacket.SourcePort.ToString(),
                    DestinationPort = udpPacket.DestinationPort.ToString()
                };
                _context.udpProtocols.Add(udpModel);
                _context.SaveChanges();
            }

            captured = true;
            captureEvent.Set();
        }

 
    }

}
