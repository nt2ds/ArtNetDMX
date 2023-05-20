namespace ArtNet.Packets.Codes
{
    public enum FirmwareCodes : byte
    {
        /// <summary>
        /// The first packet of a firmware upload.
        /// </summary>
        FirmFirst = 0x00,
        /// <summary>
        /// A consecutive continuation packet of a firmware upload.
        /// </summary>
        FirmCont = 0x01,
        /// <summary>
        /// The last packet of a firmware upload.
        /// </summary>
        FirmLast = 0x02,
        /// <summary>
        /// The first packet of a UBEA upload.
        /// </summary>
        UbeaFirst = 0x03,
        /// <summary>
        /// A consecutive continuation packet of a UBEA upload.
        /// </summary>
        UbeaCont = 0x04,
        /// <summary>
        /// The last packet of a UBEA upload.
        /// </summary>
        UbeaLast = 0x05
    }
}
