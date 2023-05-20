using System;
using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    [OpCode(OpCode = OpCodes.OpIpProg)]
    public class ArtIpProg : ArtNetPacket
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
        [ArrayLength(FixedSize = 2)]
        public byte[] Filler;
        /// <summary>
        /// Defines the how this packet is processed. If all bits are clear, this is an enquiry only.
        /// </summary>
        /// <remarks>
        /// Refer to the ArtNet <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf#page=38">User Guide</see> on how to use.
        /// </remarks>
        public byte Command;
        /// <summary>
        /// Set to zero. Pads data structure for word alignment
        /// </summary>
        public byte Filler1;
        /// <summary>
        /// IP Address to be programmed into Node if enabled by Command Field
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] ProgIp;
        /// <summary>
        /// Subnet mask to be programmed into Node if enabled by Command Field
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] ProgSm;
        [Obsolete("(Deprecated)")]
        [SkipBin2Object]
        public byte[] ProgPort;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 8)]
        public byte[] Spare;

        public ArtIpProg() : base(OpCodes.OpIpProg)
        {

        }

        public static new ArtIpProg FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtIpProg packet = reader.ReadObject<ArtIpProg>();

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