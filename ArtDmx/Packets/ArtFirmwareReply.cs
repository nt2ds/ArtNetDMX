using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// This packet is sent by the Node to the Controller in acknowledgement of each OpFirmwareMaster packet.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpFirmwareReply)]
    public class ArtFirmwareReply : ArtNetPacket
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
        /// Defines the packet contents as follows.
        /// Codes are used for both firmware and UBEA.
        /// </summary>
        public FirmwareReplyCodes Type;
        /// <summary>
        /// Controller sets to zero, Node does not test.
        /// </summary>
        [ArrayLength(FixedSize = 21)]
        public byte[] Spare;


        public ArtFirmwareReply() : base(OpCodes.OpFirmwareReply)
        {

        }

        public static new ArtFirmwareReply FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtFirmwareReply packet = reader.ReadObject<ArtFirmwareReply>();

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