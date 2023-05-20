using System;
using System.IO;
using System.Reflection;
using System.Text;
using static ArtNet.Attributes;
using ArtNet.IO;
using ArtNet.Packets.Codes;
using NoisyCowStudios.Bin2Object;

namespace ArtNet.Packets
{
    public class ArtNetPacket
    {
        [SkipBin2Object]
        public ArtNetData PacketData;
        /// <summary>
        /// Array of 8 characters, the final character is a null termination.
        /// </summary>
        /// <value>
        ///  ["A", "r", "t", "-", "N", "e", "t", 0x00]
        /// </value>
        [SkipBin2Object]
        public byte[] ID;
        /// <summary>
        /// The OpCode defines the class of data following ArtPoll within this UDP packet. 
        /// Transmitted low byte first. 
        /// <para>See <see cref="OpCodes">OpCodes</see>. Set to OpPoll.</para>
        /// </summary>
        [SkipBin2Object]
        public OpCodes OpCode;


        /// <summary>
        /// Initialize ArtNetPacket with <see cref="OpCodes">OpCode</see>
        /// </summary>
        public ArtNetPacket(OpCodes opCode)
        {
            ID = new byte[] { 65, 114, 116, 45, 78, 101, 116, 0, }; // "Art-Net" as null terminated byte array
            OpCode = opCode;
        }


        public static ArtNetPacket FromData(ArtNetData data)
        {
            var stream = new MemoryStream(data.Buffer);
            var reader = new BinaryObjectReader(stream);

            byte[] id = reader.ReadBytes(8);
            OpCodes code = (OpCodes)reader.ReadInt16();

            ArtNetPacket packet = new(code);
            packet.ID = id;
            packet.PacketData = data;

            return packet;
        }

        /// <summary>
        /// mary>
        /// Writes all <see cref="ArtNetPacket"/> data to a byte array
        /// </summary>
        /// <returns>A byte array containing all <see cref="ArtNetPacket"/> data</returns>
        public virtual byte[] ToArray()
        {
            var stream = new MemoryStream();
            var writer = new BinaryObjectWriter(stream);
            writer.Write(ID);
            writer.Write((short)OpCode);

            writer.WriteObject(this);
            return stream.ToArray();
        }

        /// <summary>
        /// Casts an <see cref="ArtNetPacket"/> to another packet that implements <see cref="ArtNetPacket"/> and has the correct OpCode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Cast<T>(bool force = false)
        {
            if (!typeof(T).IsSubclassOf(typeof(ArtNetPacket)) && !force) {
                throw new ArgumentException("Overloaded type is not a subclass of ArtNetPacket");
            }

            if (!(typeof(T).GetCustomAttribute<OpCodeAttribute>(false) is OpCodeAttribute) && !force)
            {
                throw new ArgumentException("Overloaded type does not have the OpCodeAttribute");
            }

            if(typeof(T).GetCustomAttribute<OpCodeAttribute>(false).OpCode != OpCode && !force)
            {
                throw new ArgumentException("Overloaded type's OpCode does not match packet OpCode");
            }

            return (T)typeof(T).GetMethod("FromData")?.Invoke(null, new object[] { PacketData });
        }

        /// <summary>
        /// Converts a <see cref="ArtNetPacket"/> to a string
        /// </summary>
        /// <returns>An <see cref="ArtNetPacket"/> formatted as string </returns>
        public override string ToString()
        {
            return $"ID: {Encoding.UTF8.GetString(ID)}\nOpCode: {Enum.GetName(typeof(OpCodes), OpCode)}({OpCode:X})\n";
        }
    }
}
