using System;
using System.Collections.Generic;
using ArtDotNet;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace ArtNetDMX
{
    public class ArtNetC
    {
        public static int dmxChannel = 0;
        public static byte channelVal = 0;
        public static Form1 mainWin;
        public static string recvIpAddress = "";
        public static int recvUni = 0;
        public static bool running = true;
        public static void startArtNet()
        {
            ArtNetController controller = new ArtNetController();
            controller.Address = IPAddress.Parse(recvIpAddress);
            controller.DmxPacketReceived += (s, p) =>
            {

                if (p.SubUni != recvUni)
                    return;

                for (int i = 0; i < p.Length; i++)
                {
                    mainWin.CallDmxUpdate(i, p.Data[i]);
                }

            };
            controller.Start();
        }
    }
}
