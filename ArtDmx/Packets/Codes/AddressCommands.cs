namespace ArtNet.Packets.Codes
{
    /// <summary>
    /// Node configuration commands for <see cref="ArtAddress"/>
    /// </summary>
    public enum AddressCommands : byte
    {
        /// <summary>
        /// No action
        /// </summary>
        AcNone = 0x00,
        /// <summary>
        /// If Node is currently in merge mode, cancel merge mode upon receipt of next ArtDmx packet. 
        /// </summary>
        /// <remarks>
        /// See <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf#page=62">discussion</see> of merge mode operation.
        /// </remarks>
        AcCancelMerge = 0x01,
        /// <summary>
        /// The front panel indicators of the Node operate normally
        /// </summary>
        AcLedNormal = 0x02,
        /// <summary>
        /// The front panel indicators of the Node are disabled and switched off
        /// </summary>
        AcLedMute = 0x03,
        /// <summary>
        /// Rapid flashing of the Node"s front panel indicators.
        /// It is intended as an outlet identifier for large installations.
        /// </summary>
        AcLedLocate = 0x04,
        /// <summary>
        /// Resets the Node"s Sip, Text, Test and data error flags.
        /// If an output short is being flagged, forces the test to re-run.
        /// </summary>
        AcResetRxFlags = 0x05,

        // Fail-over configuration commands: These settings should be retained by the node during power cycling

        /// <summary>
        /// Set the node to hold last state in the event of loss of network data.
        /// </summary>
        AcFailHold = 0x08,
        /// <summary>
        /// Set the node"s outputs to zero in the event of loss of network data.
        /// </summary>
        AcFailZero = 0x09,
        /// <summary>
        /// Set the node’s outputs to zero in the event of loss of network data.
        /// </summary>
        AcFailFull = 0x0a,
        /// <summary>
        /// Set the node’s outputs to play the fail-over scene in the event of loss of network data.
        /// </summary>
        AcFailScene = 0x0b,
        /// <summary>
        /// Record the current output state as the fail-over scene.
        /// </summary>
        AcFailRecord = 0x0c,

        // Node configuration commands: Note that Ltp / Htp settings should be retained by the node during power cycling.
        /// <summary>
        /// Set DMX Port 0 to Merge in LTP mode.
        /// </summary>
        AcMergeLtp0 = 0x10,
        /// <summary>
        /// Set DMX Port 1 to Merge in LTP mode.
        /// </summary>
        AcMergeLtp1 = 0x11,
        /// <summary>
        /// Set DMX Port 2 to Merge in LTP mode.
        /// </summary>
        AcMergeLtp2 = 0x12,
        /// <summary>
        /// Set DMX Port 3 to Merge in LTP mode.
        /// </summary>
        AcMergeLtp3 = 0x13,
        /// <summary>
        /// Set DMX Port 0 to Merge in HTP (default) mode.
        /// </summary>
        AcMergeHtp0 = 0x50,
        /// <summary>
        /// Set DMX Port 1 to Merge in HTP (default) mode.
        /// </summary>
        AcMergeHtp1 = 0x51,
        /// <summary>
        /// Set DMX Port 2 to Merge in HTP (default) mode.
        /// </summary>
        AcMergeHtp2 = 0x52,
        /// <summary>
        /// Set DMX Port 3 to Merge in HTP (default) mode.
        /// </summary>
        AcMergeHtp3 = 0x53,
        /// <summary>
        /// Set DMX Port 0 to output both DMX512 and RDM packets from the Art-Net protocol (default).
        /// </summary>
        AcArtNetSel0 = 0x60,
        /// <summary>
        /// Set DMX Port 1 to output both DMX512 and RDM packets from the Art-Net protocol (default).
        /// </summary>
        AcArtNetSel1 = 0x61,
        /// <summary>
        /// Set DMX Port 2 to output both DMX512 and RDM packets from the Art-Net protocol (default).
        /// </summary>
        AcArtNetSel2 = 0x62,
        /// <summary>
        /// Set DMX Port 3 to output both DMX512 and RDM packets from the Art-Net protocol (default).
        /// </summary>
        AcArtNetSel3 = 0x63,
        /// <summary>
        /// Set DMX Port 0 to output DMX512 data from the sACN protocol and RDM data from the Art-Net protocol.
        /// </summary>
        AcAcnSel0 = 0x70,
        /// <summary>
        /// Set DMX Port 1 to output DMX512 data from the sACN protocol and RDM data from the Art-Net protocol.
        /// </summary>
        AcAcnSel1 = 0x71,
        /// <summary>
        /// Set DMX Port 2 to output DMX512 data from the sACN protocol and RDM data from the Art-Net protocol.
        /// </summary>
        AcAcnSel2 = 0x72,
        /// <summary>
        /// Set DMX Port 3 to output DMX512 data from the sACN protocol and RDM data from the Art-Net protocol.
        /// </summary>
        AcAcnSel3 = 0x73,
        /// <summary>
        /// Clear DMX Output buffer for Port 0.
        /// </summary>
        AcClearOp0 = 0x90,
        /// <summary>
        /// Clear DMX Output buffer for Port 1.
        /// </summary>
        AcClearOp1 = 0x91,
        /// <summary>
        /// Clear DMX Output buffer for Port 2.
        /// </summary>
        AcClearOp2 = 0x92,
        /// <summary>
        /// Clear DMX Output buffer for Port 3.
        /// </summary>
        AcClearOp3 = 0x93,
        /// <summary>
        /// Set output style to delta mode (DMX frame triggered by ArtDmx) for Port 0.
        /// </summary>
        AcStyleDelta0 = 0xa0,
        /// <summary>
        /// Set output style to delta mode (DMX frame triggered by ArtDmx) for Port 1.
        /// </summary>
        AcStyleDelta1 = 0xa1,
        /// <summary>
        /// Set output style to delta mode (DMX frame triggered by ArtDmx) for Port 2.
        /// </summary>
        AcStyleDelta2 = 0xa2,
        /// <summary>
        /// Set output style to delta mode (DMX frame triggered by ArtDmx) for Port 3.
        /// </summary>
        AcStyleDelta3 = 0xa3,
        /// <summary>
        /// Set output style to constant mode (DMX output is continuous) for Port 0.
        /// </summary>
        AcStyleConst0 = 0xb0,
        /// <summary>
        /// Set output style to constant mode (DMX output is continuous) for Port 1.
        /// </summary>
        AcStyleConst1 = 0xb1,
        /// <summary>
        /// Set output style to constant mode (DMX output is continuous) for Port 2.
        /// </summary>
        AcStyleConst2 = 0xb2,
        /// <summary>
        /// Set output style to constant mode (DMX output is continuous) for Port 3.
        /// </summary>
        AcStyleConst3 = 0xb3,
        /// <summary>
        /// Enable RDM for Port 0.
        /// </summary>
        AcRdmEnable0 = 0xc0,
        /// <summary>
        /// Enable RDM for Port 1.
        /// </summary>
        AcRdmEnable1 = 0xc1,
        /// <summary>
        /// Enable RDM for Port 2.
        /// </summary>
        AcRdmEnable2 = 0xc2,
        /// <summary>
        /// Enable RDM for Port 3.
        /// </summary>
        AcRdmEnable3 = 0xc3,
        /// <summary>
        /// Disable RDM for Port 0.
        /// </summary>
        AcRdmDisable0 = 0xd0,
        /// <summary>
        /// Disable RDM for Port 1.
        /// </summary>
        AcRdmDisable1 = 0xd1,
        /// <summary>
        /// Disable RDM for Port 2.
        /// </summary>
        AcRdmDisable2 = 0xd2,
        /// <summary>
        /// Disable RDM for Port 3.
        /// </summary>
        AcRdmDisable3 = 0xd3,
    }
}
