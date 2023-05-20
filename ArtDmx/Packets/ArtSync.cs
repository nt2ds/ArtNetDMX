using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// The ArtSync packet can be used to force nodes to synchronously output ArtDmx packets to their outputs.
    /// This is useful in video and media-wall applications.
    /// A controller that wishes to implement synchronous transmission will unicast multiple universes of ArtDmx and then broadcast an ArtSync to synchronously transfer all the ArtDmx packets to the nodes’ outputs at the same time.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpSync)]
    public class ArtSync : ArtNetPacket
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
        /// Transmit as zero
        /// </summary>
        [ArrayLength(FixedSize = 2)]
        public byte Aux;

        public ArtSync() : base(OpCodes.OpSync)
        {

        }

        public static new ArtSync FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtSync packet = reader.ReadObject<ArtSync>();

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