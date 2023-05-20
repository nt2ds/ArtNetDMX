using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// ArtVlc is a specific implementation of the ArtNzs packet which is used for the transfer of VLC (Visible Light Communication) data over Art-Net.
    /// </summary>
    /// <remarks>
    /// The packet’s payload can also be used to transfer VLC over a DMX512 physical layer.
    /// </remarks>
    [OpCode(OpCode = OpCodes.OpNzs)]
    public class ArtVlc : ArtNetPacket
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
        /// The sequence number is used to ensure that ArtNzs packets are used in the correct order.
        /// When Art-Net is carried over a medium such as the Internet, it is possible that ArtNzs packets will reach the receiver out of order.
        /// This field is incremented in the range 0x01 to 0xff to allow the receiving node to resequence packets.
        /// The Sequence field is set to 0x00 to disable this feature.
        /// </summary>
        public byte Sequence;
        /// <summary>
        /// The DMX512 start code of this packet is set to 91.
        /// No other values are allowed.
        /// </summary>
        public byte StartCode;
        /// <summary>
        /// The low byte of the 15 bit Port-Address to which this packet is destined.
        /// </summary>
        public byte SubUni;
        /// <summary>
        /// The top 7 bits of the 15 bit Port-Address to which this packet is destined.
        /// </summary>
        public byte Net;
        /// <summary>
        /// The length of the Vlc data array.
        /// This value should be in the range 1 – 512.
        /// It represents the number of DMX512 channels encoded in packet.
        /// High Byte.
        /// </summary>
        public byte LengthHi;
        /// <summary>
        /// Low Byte of above.
        /// </summary>
        public byte LengthLo;
        /// <summary>
        /// A variable length array of VLC data as in the <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf#page=69">User Guide</see>.
        /// </summary>
        [ArrayLength(FieldName = "Length")]
        public byte[] Vlc;

        public int Length
        {
            get => LengthLo | LengthHi << 8;
            set
            {
                LengthLo = (byte)(value & 0xFF);
                LengthHi = (byte)(value >> 8);
            }
        }

        public ArtVlc() : base(OpCodes.OpNzs)
        {

        }

        public static new ArtVlc FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtVlc packet = reader.ReadObject<ArtVlc>();

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

        public override string ToString()
        {
            return base.ToString() +
                $"Data Length: {Length}\n";
        }
    }
}
