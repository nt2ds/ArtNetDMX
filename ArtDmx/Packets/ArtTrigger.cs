using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtTrigger packet is used to send trigger macros to the network.
    /// The most common implementation involves a single controller broadcasting to all other devices.
    /// In some circumstances a controller may only wish to trigger a single device or a small group in which case unicast would be used.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpTrigger)]
    public class ArtTrigger : ArtNetPacket
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
        /// Ignore by receiver, set to zero by sender
        /// </summary>
        [ArrayLength(FixedSize = 2)]
        public byte Filler;
        /// <summary>
        /// The manufacturer code (high byte) of nodes that shall accept this trigger.
        /// </summary>
        public byte OemCodeHi;
        /// <summary>
        /// The manufacturer code (low byte) of nodes that shall accept this trigger
        /// </summary>
        public byte OemCodeLo;
        /// <summary>
        /// The Trigger Key.
        /// </summary>
        public byte Key;
        /// <summary>
        /// The Trigger SubKey.
        /// </summary>
        public byte SubKey;
        /// <summary>
        /// The interpretation of the payload is defined by the Key.
        /// </summary>
        [ArrayLength(FixedSize = 512)]
        public byte[] Data;

        public ArtTrigger() : base(OpCodes.OpTrigger)
        {

        }

        public static new ArtTrigger FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtTrigger packet = reader.ReadObject<ArtTrigger>();

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