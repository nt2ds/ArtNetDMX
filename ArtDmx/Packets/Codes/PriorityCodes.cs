namespace ArtNet.Packets.Codes
{
    /// <summary>
    /// The following enum contains the Priority codes.
    /// <para> These are used in ArtPoll and ArtDiagData.</para>
    /// </summary>
    /// /// <remarks>
    /// See the ArtNet <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf">User Guide</see> for more.
    /// </remarks>
    public enum PriorityCodes : byte
    {
        /// <summary>
        /// Low priority message
        /// </summary>
        DpLow = 0x10,
        /// <summary>
        /// Medium priority message.
        /// </summary>
        DpMed = 0x40,
        /// <summary>
        /// High priority message.
        /// </summary>
        DpHigh = 0x80,
        /// <summary>
        /// Critical priority message.
        /// </summary>
        DpCritical = 0xe0,
        /// <summary>
        /// Volatile message. 
        /// Messages of this type are displayed on a single line in the DMX-Workshop diagnostics display. 
        /// All other types are displayed in a list box.
        /// </summary>
        DpVolatile = 0xf0
    }
}
