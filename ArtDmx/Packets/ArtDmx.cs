using System;
using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    [OpCode(OpCode = OpCodes.OpDmx)]
    public class ArtDmx : ArtNetPacket
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
        /// The sequence number is used to ensure that ArtDmx packets are used in the correct order. 
        /// When Art-Net is carried over a medium such as the Internet, it is possible that ArtDmx packets will reach the receiver out of order. 
        /// This field is incremented in the range 0x01 to 0xff to allow the receiving node to resequence packets.
        /// The Sequence field is set to 0x00 to disable this feature
        /// </summary>
        public byte Sequence;
        /// <summary>
        /// The physical input port from which DMX512 data was input. 
        /// This field is for information only.
        /// Use Universe for data routing.
        /// </summary>
        public byte Physical;
        /// <summary>
        /// The low byte of the 15 bit Port-Address to which this packet is destined.
        /// </summary>
        public byte SubUni;
        /// <summary>
        /// The top 7 bits of the 15 bit Port-Address to which this packet is destined.
        /// </summary>
        public byte Net;
        /// <summary>
        /// The length of the DMX512 data array. This value should be an even number in the range 2 – 512. 
        /// It represents the number of DMX512 channels encoded in packet.NB: Products which convert Art-Net to DMX512 may opt to always send 512 channels. 
        /// </summary>
        public byte LengthHi;
        /// <summary>
        /// Low Byte of above.
        /// </summary>
        public byte LengthLo;
        /// <summary>
        /// A variable length array of DMX512 lighting data.
        /// </summary>
        /// <remarks> Set length fixed to 512 bytes</remarks>
        [ArrayLength(FieldName = "Length")]
        public byte[] Data;

        public int Length
        {
            get => LengthLo | LengthHi << 8;
            set
            {
                LengthLo = (byte)(value & 0xFF);
                LengthHi = (byte)(value >> 8);
            }
        }

        public int Universe
        {
            get => SubUni | Net << 8;
            set
            {
                SubUni = (byte)(value & 0xFF);
                Net = (byte)(value >> 8);
            }
        }

        public ArtDmx() : base(OpCodes.OpDmx)
        {

        }

        public static new ArtDmx FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtDmx packet = reader.ReadObject<ArtDmx>();

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
                $"Universe: {Universe}\n" +
                $"Data Length: {Length}\n";
        }
    }
}
