using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// ArtTimeCode allows time code to be transported over the network.
    /// The data format is compatible with both longitudinal time code and MIDI time code.
    /// The four key types of Film, EBU, Drop Frame and SMPTE are also encoded.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpTimeCode)]
    public class ArtTimeCode : ArtNetPacket
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
        /// Frames time. 0 – 29 depending on mode
        /// </summary>
        public byte Frames;
        /// <summary>
        /// Seconds. 0 - 59.
        /// </summary>
        public byte Seconds;
        /// <summary>
        /// Minutes. 0 - 59.
        /// </summary>
        public byte Minutes;
        /// <summary>
        /// Hours. 0 - 23.
        /// </summary>
        public byte Hours;
        /// <summary>
        /// Video type <see cref="TimeCodes"/>
        /// </summary>
        public TimeCodes Type;

        public ArtTimeCode() : base(OpCodes.OpTimeCode)
        {

        }

        public static new ArtTimeCode FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtTimeCode packet = reader.ReadObject<ArtTimeCode>();

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