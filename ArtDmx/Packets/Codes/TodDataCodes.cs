using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Art.Net.Packets.Codes
{
    public enum TodDataCodes : byte
    {
        /// <summary>
        /// The packet contains the entire TOD or is the first packet in a sequence of packets that contains the entire TOD.
        /// </summary>
        TodFull = 0x00,
        /// <summary>
        /// The TOD is not available or discovery is incomplete.
        /// </summary>
        TodNak = 0xff
    }
}
