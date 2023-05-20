using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    [OpCode(OpCode = OpCodes.OpTodRequest)]
    public class ArtTodRequest : ArtNetPacket
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
        public short Filler;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 7)]
        public byte[] Spare;
        /// <summary>
        /// The top 7 bits of the 15 bit Port-Address of Nodes that must respond to this packet.
        /// </summary>
        public byte Net;
        /// <summary>
        /// Defines the packet contents.
        /// </summary>
        public TodRequestCodes Command;
        /// <summary>
        /// The number of entries in Address that are used. Max value is 32.
        /// </summary>
        public byte AddCount;
        /// <summary>
        /// This array defines the low byte of the PortAddress of the Output Gateway nodes that must respond to this packet.
        /// The high nibble is the Sub-Net switch. 
        /// The low nibble corresponds to the Universe.
        /// This is combined with the 'Net' field above to form the 15 bit address.
        /// </summary>
        [ArrayLength(FixedSize = 32)]
        public byte[] Address;

        public int ProtVer
        {
            get => ProtVerLo | ProtVerHi << 8;
            set
            {
                ProtVerLo = (byte)(value & 0xFF);
                ProtVerHi = (byte)(value >> 8);
            }
        }

        public ArtTodRequest() : base(OpCodes.OpTodRequest)
        {

        }

        public static new ArtTodRequest FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtTodRequest packet = reader.ReadObject<ArtTodRequest>();

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