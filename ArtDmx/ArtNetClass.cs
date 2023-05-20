using ArtNet.IO;
using ArtNet.Packets;
using ArtNet.Packets.Codes;
using ArtNet.Sockets;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace ArtNetToDMX
{
    class ArtNetClass
    {

        public static ArtNet_to_DMX mainWin;
        public static ArtNetSocket artnet;
        public static void StartArtNet(string address)
        {
            artnet = new ArtNetSocket
            {
                EnableBroadcast = true
            };
            var localIP = IPAddress.Parse(address);

            /*Console.WriteLine(artnet.GetBroadcastAddress());
            Console.WriteLine(localIP.ToString());*/

            artnet.Begin(localIP, IPAddress.Parse("255.255.255.0"));
            artnet.NewPacket += (object sender, ArtNet.Sockets.NewPacketEventArgs<ArtNetPacket> e) =>
            {
                ArtDmx dmx;
                //Console.WriteLine(e.Packet.ToString());
                if (e.Packet.OpCode == OpCodes.OpDmx)
                {
                    dmx = ArtDmx.FromData(e.Packet.PacketData);
                    /*Console.WriteLine(dmx.ToString());
                    Console.WriteLine(dmx.Data[511]);*/
                    mainWin.datastream(dmx);
                }
                if (e.Packet.OpCode == OpCodes.OpPoll)
                {
                    
                    var pollReply = new ArtPollReply
                    {
                        IP = localIP.GetAddressBytes(),
                        Port = (short)artnet.Port,
                        Mac = NetworkInterface.GetAllNetworkInterfaces()
                            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                            .Select(nic => nic.GetPhysicalAddress().GetAddressBytes())
                            .FirstOrDefault(),

                        GoodInput = new byte[] { 0x08, 0x08, 0x08, 0x08 },
                        GoodOutput = new byte[] { 0x80, 0x80, 0x80, 0x80 },
                        PortTypes = new byte[] { 0xc0, 0xc0, 0xc0, 0xc0 },
                        ShortName = "Art.Net\0",
                        LongName = "C# Art-Net to DMX\0",
                        VersInfoH = 6,
                        VersInfoL = 9,
                        Oem = 0xFFFF,
                        Status1 = 0xd2,
                        Style = (byte)StyleCodes.StNode,
                        NumPortsLo = 4,
                        Status2 = 0x08,
                        BindIp = localIP.GetAddressBytes(),
                        SwIn = new byte[] { 0x01, 0x02, 0x03, 0x04 },
                        SwOut = new byte[] { 0x01, 0x02, 0x03, 0x04 },
                        GoodOutput2 = new byte[] { 0x80, 0x80, 0x80, 0x80 },

                        NodeReport = "Up and running\0",
                        Filler = new byte[168]
                    };

                    artnet.Send(pollReply);
                }

            };
            var artPoll = new ArtPoll
            {
                ProtVer = 14,
                Priority = PriorityCodes.DpLow
            };

            artnet.SendWithInterval(artPoll, 3000);
        }

        public static void StopArtNet()
        {
            artnet.Close();
        }
    }
}
