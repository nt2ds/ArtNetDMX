using System;
using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// A Controller or monitoring device on the network can reprogram numerous controls of a node remotely.
    /// This, for example, would allow the lighting console to re-route DMX512 data at remote locations. 
    /// This is achieved by sending an <see cref="ArtDiagData"/> packet to the Node’s IP address. (The IP address is returned in the <see cref="ArtPoll"/> packet). 
    /// The node replies with an <see cref="ArtPollReply"/> packet.
    /// </summary>
    [OpCode(OpCode = OpCodes.OpAddress)]
    public class ArtDiagData : ArtNetPacket
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
        public byte Filler1;
        /// <summary>
        /// The priority of this diagnostic data. 
        /// </summary>
        /// <remarks>
        /// See <see cref="PriorityCodes"/>
        /// </remarks>
        public PriorityCodes Priority;
        /// <summary>
        /// Ignore by receiver, set to zero by sender
        /// </summary>
        [ArrayLength(FixedSize = 2)]
        public byte[] Filler2;
        /// <summary>
        /// The length of the text array below. High Byte
        /// </summary>
        public byte LengthHi;
        /// <summary>
        /// Low Byte.
        /// </summary>
        public byte LengthLo;
        /// <summary>
        /// ASCII text array, null terminated. Max length is 512 bytes including the null terminator.
        /// </summary>
        [String(FixedSize = 512)]
        public string Data;

        public int Length
        {
            get => LengthLo | LengthHi << 8;
            set
            {
                LengthLo = (byte)(value & 0xFF);
                LengthHi = (byte)(value >> 8);
            }
        }

        public ArtDiagData() : base(OpCodes.OpDiagData)
        {

        }

        public static new ArtDiagData FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10
            };

            ArtDiagData packet = reader.ReadObject<ArtDiagData>();

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