using System;
using System.Net;

namespace ArtNet.Sockets
{
    public class NewPacketEventArgs<TPacketType> : EventArgs
    {
        public NewPacketEventArgs(IPEndPoint source, TPacketType packet)
        {
            Source = source;
            Packet = packet;
        }

        public IPEndPoint Source;

        public TPacketType Packet;

    }
}