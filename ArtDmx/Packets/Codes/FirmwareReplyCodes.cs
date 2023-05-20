namespace ArtNet.Packets.Codes
{
    public enum FirmwareReplyCodes : byte
    {
        /// <summary>
        /// Last packet received successfully.
        /// </summary>
        FirmBlockGood = 0x00,
        /// <summary>
        /// Good All firmware received successfully.
        /// </summary>
        FirmAll = 0x01,
        /// <summary>
        /// Firmware upload failed.
        /// (All error conditions).
        /// </summary>
        FirmFail = 0xff
    }
}
