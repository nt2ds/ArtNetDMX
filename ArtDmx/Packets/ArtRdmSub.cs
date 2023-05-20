using System.IO;
using System.Linq;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using ArtNet.Rdm;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtRdmSub packet is used to send RDM control parameters over Art-Net.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpRdmSub)]
    public class ArtRdmSub : ArtNetPacket
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
        /// Devices that support RDM STANDARD V1.0 set field to 0x01.
        /// </summary>
        public byte RdmVer;
        /// <summary>
        /// Pad length to match ArtPoll.
        /// </summary>
        public byte Filler;
        /// <summary>
        /// UID of target RDM device.
        /// </summary>
        public UId UID;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        public byte Spare1;
        /// <summary>
        /// As per RDM specification.
        /// This field defines whether this is a Get, Set, GetResponse, SetResponse.
        /// </summary>
        public byte CommandClass;
        /// <summary>
        /// The low 8 bits of the Port-Address of the Output Gateway DMX Port that generated this packet.
        /// The high nibble is the Sub-Net switch. 
        /// The low nibble corresponds to the Universe.
        /// </summary>
        public short ParameterId;
        /// <summary>
        /// Defines the first device information contained in packet.
        /// This follows the RDM convention that 0 = root device and 1 = first subdevice.
        /// Big-endian.
        /// </summary>
        public short SubDevice;
        /// <summary>
        /// The number of sub devices packed into packet.
        /// Zero is illegal.
        /// Big-endian.
        /// </summary>
        public short SubCount;
        /// <summary>
        /// Transmit as zero, receivers don’t test.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] Spare2;
        /// <summary>
        /// Packed 16-bit big-endian data.
        /// The size of the data array is defined by the contents of CommandClass and SubCount:.
        /// </summary>
        [ArrayLength(FieldName = "SubCount")]
        public short[] Data;

        public ArtRdmSub() : base(OpCodes.OpRdmSub)
        {

        }

        public static new ArtRdmSub FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtRdmSub packet = reader.ReadObject<ArtRdmSub>();

            packet.SubCount = (short)((packet.SubCount << 8) + (packet.SubCount >> 8));
            packet.SubDevice = (short)((packet.SubDevice << 8) + (packet.SubDevice >> 8));
            packet.Data = packet.Data.Reverse().ToArray();

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