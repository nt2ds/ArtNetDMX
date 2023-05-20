namespace ArtNet.Packets.Codes
{
    public enum TimeCodes : byte
    {
        /// <summary>
        /// 24 frames per second
        /// </summary>
        Film = 0,
        /// <summary>
        /// 25 frames per second
        /// </summary>
        EBU = 1,
        /// <summary>
        /// 29.97 frames per second
        /// </summary>
        DF = 2,
        /// <summary>
        /// 30 frames per second
        /// </summary>
        SMPTE = 3
    }
}
