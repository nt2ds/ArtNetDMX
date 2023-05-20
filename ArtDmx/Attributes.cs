using System;
using ArtNet.Packets.Codes;

namespace ArtNet
{
    class Attributes
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class OpCodeAttribute : Attribute
        {
            public OpCodes OpCode { get; set; } = OpCodes.OpNone;
        }
    }
}
