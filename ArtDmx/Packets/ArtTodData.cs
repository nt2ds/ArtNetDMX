using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using ArtNet.Rdm;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    [OpCode(OpCode = OpCodes.OpTodData)]
    public class ArtTodData : ArtNetPacket
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
        /// Art-Net Devices that only support RDM DRAFT V1.0 set field to 0x00. 
        /// Devices that support RDM STANDARD V1.0 set field to 0x01
        /// </summary>
        public byte RdmVer;
        /// <summary>
        /// Physical port index. Range 1-4. This number is used in combination with BindIndex to identify the physical port that generated the packet.
        /// </summary>
        /// <remarks>
        /// Refer to the ArtNet <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf#page=85">User Guide</see> on how to use.
        /// </remarks>
        public byte Port;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 6)]
        public byte[] Spare;
        /// <summary>
        /// The BindIndex defines the bound node which originated this packet.
        /// In combination with Port and Source IP address, it uniquely identifies the sender. 
        /// This must match the BindIndex field in ArtPollReply. 
        /// This number represents the order of bound devices. 
        /// A lower number means closer to root device.
        /// A value of 1 means root device.
        /// </summary>
        public byte BindIndex;
        /// <summary>
        /// The top 7 bits of the Port-Address of the Output Gateway DMX Port that generated this packet.
        /// </summary>
        public byte Net;
        /// <summary>
        /// Defines the packet contents.
        /// </summary>
        /// <value>
        /// Either TodFull(0x00) or TodNak(0xFF, not available)
        /// </value>
        public byte CommandResponse;
        /// <summary>
        /// The low 8 bits of the Port-Address of the Output Gateway DMX Port that generated this packet.
        /// The high nibble is the Sub-Net switch. 
        /// The low nibble corresponds to the Universe.
        /// </summary>
        public byte Address;
        /// <summary>
        /// The total number of RDM devices discovered by this Universe.
        /// </summary>
        public byte UidTotalHi;
        /// <summary>
        /// The low byte of the field above
        /// </summary>
        public byte UidTotalLo;
        /// <summary>
        /// The index number of this packet. 
        /// When UidTotal exceeds 200, multiple ArtTodData packets are used. 
        /// BlockCount is set to zero for the first packet, and incremented for each subsequent packet containing blocks of TOD information.
        /// </summary>
        public byte BlockCount;
        /// <summary>
        /// The number of UIDs encoded in this packet. 
        /// This is the index of the following array.
        /// </summary>
        public byte UidCount;
        /// <summary>
        /// An array of RDM UID.
        /// </summary>
        [ArrayLength(FieldName = "UidCount")]
        public UId[] ToD;

        public ArtTodData() : base(OpCodes.OpTodData)
        {

        }

        public static new ArtTodData FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtTodData packet = reader.ReadObject<ArtTodData>();

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