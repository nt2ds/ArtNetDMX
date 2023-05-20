using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtInput packet is used to send trigger macros to the network.
    /// The most common implementation involves a single controller broadcasting to all other devices.
    /// In some circumstances a controller may only wish to trigger a single device or a small group in which case unicast would be used.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpInput)]
    public class ArtInput : ArtNetPacket
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
        public byte Filler;
        /// <summary>
        /// The BindIndex defines the bound node which originated this packet and is used to uniquely identify the bound node when identical IP addresses are in use.
        /// This number represents the order of bound devices.
        /// A lower number means closer to root device.
        /// A value of 1 means root device.
        /// </summary>
        public byte BindIndex;
        /// <summary>
        /// The high byte of the word describing the number of input or output ports.
        /// The high byte is for future expansion and is currently zero.
        /// </summary>
        public byte NumPortsHi;
        /// <summary>
        /// The low byte of the word describing the number of input or output ports.
        /// If number of inputs is not equal to number of outputs, the largest value is taken.
        /// The maximum value is 4.
        /// </summary>
        public byte NumPortsLo;
        /// <summary>
        /// This array defines input disable status of each channel.
        /// (Example = 0x01, 0x00, 0x01, 0x00 to disable first and third inputs).
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] Input;

        public ArtInput() : base(OpCodes.OpInput)
        {

        }

        public static new ArtInput FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtInput packet = reader.ReadObject<ArtInput>();

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