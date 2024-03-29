﻿namespace ArtNet.Packets.Codes
{
    public enum TodControlCodes : byte
    {
        /// <summary>
        /// No action.
        /// </summary>
        AtcNone = 0x00,
        /// <summary>
        /// The node flushes its TOD and instigates full discovery.
        /// </summary>
        AtcFlush = 0x01
    }
}
