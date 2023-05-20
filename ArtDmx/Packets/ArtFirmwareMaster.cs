using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtFirmwareMaster packet is used to send trigger macros to the network.
    /// The most common implementation involves a single controller broadcasting to all other devices.
    /// In some circumstances a controller may only wish to trigger a single device or a small group in which case unicast would be used.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpFirmwareMaster)]
    public class ArtFirmwareMaster : ArtNetPacket
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
        /// Defines the packet contents, see <see cref="FirmwareCodes"/>
        /// </summary>
        public FirmwareCodes Type;
        /// <summary>
        /// Counts the consecutive blocks of firmware upload.
        /// Starting at 0x00 for the FirmFirst or UbeaFirst packet.
        /// </summary>
        public byte BlockId;
        /// <summary>
        /// This Int64 parameter describes the total number of words (Int16) in the firmware upload plus the firmware header size.
        /// Eg a 32K word upload plus 530 words of header information == 0x00008212.
        /// This value is also the file size (in words) of the file to be uploaded.
        /// </summary>
        public long Firmware;
        /// <summary>
        /// This array defines input disable status of each channel.
        /// (Example = 0x01, 0x00, 0x01, 0x00 to disable first and third inputs).
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] Input;
        /// <summary>
        /// Controller sets to zero, Node does not test.
        /// </summary>
        [ArrayLength(FixedSize = 20)]
        public byte[] Spare;
        /// <summary>
        /// This array contains the firmware or UBEA data block.
        /// The order is hi byte first.
        /// The interpretation of this data is manufacturer specific.
        /// Final packet should be null packed if less than 512 bytes needed.
        /// </summary>
        [ArrayLength(FixedSize = 512)]
        public byte[] Data;

        public ArtFirmwareMaster() : base(OpCodes.OpFirmwareMaster)
        {

        }

        public static new ArtFirmwareMaster FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtFirmwareMaster packet = reader.ReadObject<ArtFirmwareMaster>();

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