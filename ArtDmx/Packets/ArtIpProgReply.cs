using System;
using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;


namespace ArtNet.Packets
{
    [OpCode(OpCode = OpCodes.OpIpProgReply)]
    public class ArtIpProgReply : ArtNetPacket
    {
        /// <summary>
        /// High byte of the Art-Net protocol revision number.
        /// </summary>
        public byte ProtVerHi;
        /// <summary>
        /// Low byte of the Art-Net protocol revision number.
        /// Controllers should ignore communication with nodes using a protocol version lower than current version. 
        /// </summary>
        /// <value> 14 </value>
        public byte ProtVerLo;
        /// <summary>
        /// Pad length to match ArtPoll.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] Filler;
        /// <summary>
        /// IP Address of Node.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] ProgIp;
        /// <summary>
        /// Subnet mask of Node.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] ProgSm;
        [Obsolete("(Deprecated)")]
        [SkipBin2Object]
        public byte[] ProgPort;
        /// <remarks>
        /// Refer to the ArtNet <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf#page=40">User Guide</see> on how to use.
        /// </remarks>
        public byte Status;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 7)]
        public byte[] Spare;

        public ArtIpProgReply() : base(OpCodes.OpIpProgReply)
        {

        }

        public static new ArtIpProgReply FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtIpProgReply packet = reader.ReadObject<ArtIpProgReply>();

            packet.PacketData = data;

            return packet;
        }

        public override byte[] ToArray()
        {
            var stream = new MemoryStream();
            var writer = new BinaryObjectWriter(stream);
            writer.Write(ID);
            writer.Write((short)OpCode);

            writer.WriteObject(this);
            return stream.ToArray();
        }
    }
}