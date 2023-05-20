using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtTodControl packet is used to send RDM control parameters over Art-Net.
    /// The response is ArtTodData.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpTodControl)]
    public class ArtTodControl : ArtNetPacket
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
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 7)]
        public byte[] Spare;
        /// <summary>
        /// Defines the packet contents.
        /// </summary>
        public TodControlCodes Command;
        /// <summary>
        /// The low 8 bits of the Port-Address of the Output Gateway DMX Port that generated this packet.
        /// The high nibble is the Sub-Net switch. 
        /// The low nibble corresponds to the Universe.
        /// </summary>
        public byte Address;

        public ArtTodControl() : base(OpCodes.OpTodControl)
        {

        }

        public static new ArtTodControl FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtTodControl packet = reader.ReadObject<ArtTodControl>();

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