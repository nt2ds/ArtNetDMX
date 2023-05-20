using System;
using System.IO;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    public struct TalkToMe
    {
        public bool VLC;
        /// <summary> 
        /// <para>true = Diagnostics messages are unicast.</para>
        /// <para>false = Diagnostics messages are broadcast.</para>
        /// </summary>
        public bool DiagCast;
        public bool SendDiagnostics;
        /// <summary> 
        /// <para>true = Send ArtPollReply whenever Node conditions change. This selection allows the Controller to be informed of changes without the need to continuously poll.</para>
        /// <para>false = Only send ArtPollReply in response to an ArtPoll or ArtAddress.</para>
        /// </summary>
        public bool ReplyOnChange;
    }

    [OpCode(OpCode = OpCodes.OpPoll)]
    public class ArtPoll : ArtNetPacket
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
        /// Set behaviour of Node
        /// </summary>
        [SkipBin2Object]
        public TalkToMe TalkToMe;
        /// <summary>
        /// Holds the Flags stored in TalkToMe
        /// </summary>
        public byte Flags;
        /// <summary>
        /// The lowest priority of diagnostics message that should be sent. See <see cref="PriorityCodes"/>.
        /// </summary>
        public PriorityCodes Priority;

        public int ProtVer
        {
            get => ProtVerLo | ProtVerHi << 8;
            set
            {
                ProtVerLo = (byte)(value & 0xFF);
                ProtVerHi = (byte)(value >> 8);
            }
        }

        public ArtPoll() : base(OpCodes.OpPoll)
        {

        }

        public static new ArtPoll FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10,
            };

            ArtPoll packet = reader.ReadObject<ArtPoll>();
            packet.TalkToMe = new TalkToMe
            {
                VLC = (packet.Flags & (1 << 4)) > 0,
                DiagCast = (packet.Flags & (1 << 3)) > 0,
                SendDiagnostics = (packet.Flags & (1 << 2)) > 0,
                ReplyOnChange = (packet.Flags & (1 << 1)) > 0,
            };
            packet.PacketData = data;
            return packet;
        }


        public override byte[] ToArray()
        {
            Flags = 0;
            if (TalkToMe.VLC)
            {
                Flags |= (1 << 4);
            }
            if (TalkToMe.DiagCast)
            {
                Flags |= (1 << 3);
            }
            if (TalkToMe.SendDiagnostics)
            {
                Flags |= (1 << 2);
            }
            if (TalkToMe.ReplyOnChange)
            {
                Flags |= (1 << 1);
            }

            var stream = new MemoryStream();
            var writer = new BinaryObjectWriter(stream);
            writer.Write(ID);
            writer.Write((short)OpCode);

            writer.WriteObject(this);
            return stream.ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + $"ProtVerHi: {ProtVerHi}\nProtVerLo: {ProtVerLo}\n" +
                $"TalkToMe:\n" +
                $"\tVLC: {(TalkToMe.VLC ? "true" : "false")}\n" +
                $"\tDiagCast: {(TalkToMe.DiagCast ? "unicast" : "broadcast")}\n" +
                $"\tSendDiagnostics: {(TalkToMe.SendDiagnostics ? "true" : "false")}\n" +
                $"\tReplyOnChange: {(TalkToMe.ReplyOnChange ? "true" : "false")}\n" +
                $"Priority: {Enum.GetName(typeof(PriorityCodes), Priority)}({Priority:X})\n";
        }
    }
}