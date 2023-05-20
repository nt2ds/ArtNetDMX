using System;
using System.IO;
using System.Text;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    /// <summary>
    /// A device, in response to a Controller’s ArtPoll, sends the ArtPollReply. 
    /// This packet is also broadcast to the Directed Broadcast address by all Art-Net devices on power up
    /// </summary>
    [OpCode(OpCode = OpCodes.OpPollReply)]
    public class ArtPollReply : ArtNetPacket
    {
        /// <summary>
        /// Array containing the Node’s IP address.
        /// First array entry is most significant byte of address.
        /// When binding is implemented, bound nodes may share the root node’s IP Address and the BindIndex is used to differentiate the nodes.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] IP;
        /// <summary>
        /// The Port is always 0x1936 
        /// Transmitted low byte first.
        /// </summary>
        public short Port;
        /// <summary>
        /// High byte of Node’s firmware revision number.
        /// The Controller should only use this field to decide if a firmware update should proceed.
        /// The convention is that a higher number is a more recent release of firmware
        /// </summary>
        public byte VersInfoH;
        /// <summary>
        /// Low byte of Node’s firmware revision number.
        /// </summary>
        public byte VersInfoL;
        /// <summary>
        /// Bits 14-8 of the 15 bit Port-Address are encoded into the bottom 7 bits of this field. 
        /// This is used in combination with SubSwitch and SwIn[] or SwOut[] to produce the fulluniverse address
        /// </summary>
        public byte NetSwitch;
        /// <summary>
        /// Bits 7-4 of the 15 bit Port-Address are encoded into the bottom 4 bits of this field. 
        /// This is used in combination with NetSwitch and SwIn[] or SwOut[] to produce the full universe address.
        /// </summary>
        public byte SubSwitch;
        /// <summary>
        /// The high byte of the Oem value. 
        /// </summary>
        public byte OemHi;
        /// <summary>
        /// The low byte of the Oem value. The Oem word describes the equipmen vendor and the feature set available. 
        /// Bit 15 high indicates extended features available.
        /// Current registered codes are defined in 'Art-NetOemCodes.h' from the DMX-Workshop SDK.
        /// </summary>
        public byte OemLo;
        /// <summary>
        /// This field contains the firmware version of the User Bios Extension Area(UBEA). 
        /// If the UBEA is not programmed, this field contains zero.
        /// </summary>
        public byte UbeaVersion;
        /// <summary>
        /// General Status register containing bit fields.
        /// </summary>
        public byte Status1;
        /// <summary>
        /// The ESTA manufacturer code. 
        /// These codes are used to represent equipment manufacturer.
        /// They are assigned by ESTA. 
        /// This field can be interpreted as two ASCII bytes representing the manufacturer initials
        /// </summary>
        public byte EstaManLo;
        /// <summary>
        /// High byte of above
        /// </summary>
        public byte EstaManHi;
        /// <summary>
        /// The array represents a null terminated short name for the Node.
        /// The Controller uses the ArtAddress packet to program this string.
        /// Max length is 17 characters plus the null. 
        /// This is a fixed length field, although the string it contains can be shorter than the field.
        /// </summary>
        [String(FixedSize = 18)]
        public string ShortName;
        /// <summary>
        /// The array represents a null terminated long name for the Node.
        /// The Controller uses the ArtAddress packet to program this string. 
        /// Max length is 63 characters plus the null. 
        /// This is a fixed length field, although the string it contains can be shorter than the field.
        /// </summary>
        [String(FixedSize = 64)]
        public string LongName;
        /// <summary>
        /// The array is a textual report of the Node’s operating status or operational errors.
        /// It is primarily intended for "engineering" data rather than "end user" data.
        /// The field is formatted as: "#xxxx [yyyy...] zzzzz..." xxxx is a hex status code as defined in <see cref="NodeReport"/>. 
        /// yyyy is a decimal counter that increments every time the Node sends an ArtPollResponse. 
        /// This allows the controller to monitor event changes in the Node. 
        /// zzzz is an English text string defining the status. 
        /// This is a fixed length field, although the string it contains can be shorter than the field.
        /// </summary>
        [String(FixedSize = 64)]
        public string NodeReport;
        /// <summary>
        /// The high byte of the word describing the number of input or output ports.
        /// The high byte is for future expansion and is currently zero.
        /// </summary>
        public byte NumPortsHi;
        /// <summary>
        /// The low byte of the word describing the number of input or output ports.
        /// If number of inputs is not equal to number of outputs, the largest value is taken.
        /// Zero is a legal value if no input or output ports are implemented.
        /// The maximum value is 4. 
        /// Nodes can ignore this field as the information is implicit in PortTypes[].
        /// </summary>
        public byte NumPortsLo;
        /// <summary>
        /// This array defines the operation and protocol of each channel. 
        /// A product with 4 inputs and 4 outputs would report 0xc0, 0xc0, 0xc0, 0xc0. 
        /// The array length is fixed, independent of the number of inputs or outputs physically available on the Node.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] PortTypes;
        /// <summary>
        /// This array defines input status of the node.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] GoodInput;
        /// <summary>
        /// This array defines output status of the node.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] GoodOutput;
        /// <summary>
        /// Bits 3-0 of the 15 bit Port-Address for each of the 4 possible input ports are encoded into the low nibble
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] SwIn;
        /// <summary>
        /// Bits 3-0 of the 15 bit Port-Address for each of the 4 possible output ports are encoded into the low nibble
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] SwOut;
        /// <summary>
        /// Set to 00 when video display is showing local data.
        /// Set to 01 when video is showing ethernet data.
        /// The field is now deprecated
        /// </summary>
        [Obsolete("This field is deprecated.")]
        [SkipBin2Object]
        public byte SwVideo;
        /// <summary>
        /// If the Node supports macro key inputs, this byte represents the trigger values. 
        /// The Node is responsible for ‘debouncing’ inputs. 
        /// When the <see cref="ArtPollReply"/> is set to transmit automatically, (<see cref="TalkToMe"/> Bit 1), the <see cref="ArtPollReply"/> will be sent on both key down and key up events.
        /// However, the Controller should not assume that only one bit position has changed. 
        /// The Macro inputs are used for remote event triggering or cueing. 
        /// Bit fields are active high.
        /// </summary>
        public byte SwMacro;
        /// <summary>
        /// If the Node supports remote trigger inputs, this byte represents the trigger values.
        /// The Node is responsible for ‘debouncing’ inputs. 
        /// When the <see cref="ArtPollReply"/> is set to transmit automatically, (<see cref="TalkToMe"/> Bit 1), the <see cref="ArtPollReply"/> will be sent on both key down and key up events.
        /// However, the Controller should not assume that only one bit position has changed. 
        /// The Remote inputs are used for remote event triggering or cueing. 
        /// Bit fields are active high.
        /// </summary>
        public byte SwRemote;
        /// <summary>
        /// Not used set to zero
        /// </summary>
        [ArrayLength(FixedSize = 3)]
        public byte[] Spare;
        /// <summary>
        /// The Style code defines the equipment style of the device.
        /// See <see cref="StyleCodes"/> for current Style codes.
        /// </summary>
        public StyleCodes Style;
        /// <summary>
        /// MAC Address. 
        /// Set to zero if node cannot supply this information.
        /// </summary>
        [ArrayLength(FixedSize = 6)]
        public byte[] Mac;
        /// <summary>
        /// If this unit is part of a larger or modular product, this is the IP of the root device.
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] BindIp;
        /// <summary>
        /// This number represents the order of bound devices.
        /// A lower number means closer to root device.
        /// A value of 1 means root device.
        /// </summary>
        public byte BindIndex;
        /// <summary>
        /// Controller Status register containing bit fields.
        /// </summary>
        public byte Status2;
        /// <summary>
        /// This array defines output status of the node
        /// </summary>
        [ArrayLength(FixedSize = 4)]
        public byte[] GoodOutput2;
        /// <summary>
        /// General Status register containing bit fields.
        /// </summary>
        public byte Status3;
        /// <summary>
        /// Transmit as zero. For future expansion.
        /// Size: 21 * byte(8)
        /// </summary>
        [ArrayLength(FixedSize = 168)]
        public byte[] Filler;

        public int Oem
        {
            get => OemLo | OemHi << 8;
            set
            {
                OemLo = (byte)(value & 0xFF);
                OemHi = (byte)(value >> 8);
            }
        }

        /// <summary>
        /// Initialize ArtPollReply
        /// </summary>
        public ArtPollReply() : base(OpCodes.OpPollReply)
        {

        }

        /// <summary>
        /// Writes all <see cref="ArtPollReply"/> data to a byte array
        /// </summary>
        /// <returns>A byte array containing all <see cref="ArtPollReply"/> data</returns>
        public override byte[] ToArray()
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryObjectWriter(stream);
            writer.Write(ID);
            writer.Write((short)OpCode);

            writer.WriteObject(this);

            return stream.ToArray();
        }

        public static new ArtPollReply FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream)
            {
                Position = 10,
            };

            ArtPollReply packet = reader.ReadObject<ArtPollReply>();
            packet.PacketData = data;
            return packet;
        }

        /// <summary>
        /// Converts some important information from <see cref="ArtPollReply"/> to a string
        /// </summary>
        /// <returns>S</returns>
        public override string ToString()
        {
            StringBuilder sb = new(18);
            foreach (byte b in Mac)
            {
                if (sb.Length > 0)
                    sb.Append(':');
                sb.Append(b.ToString("X2"));
            }
            return base.ToString() +
                $"IP: {IP}\n" +
                $"Port: {Port}\n" +
                $"Mac: {sb}\n" +
                $"ShortName: {ShortName}\n" +
                $"LongName: {LongName}\n" +
                $"NodeReport {NodeReport}\n" +
                $"StyleCode: {Enum.GetName(Style)}\n" +
                $"EstaManHi: {EstaManHi}";
        }
    }
}
