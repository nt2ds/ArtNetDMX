using System;

namespace ArtNet.Packets.Codes
{
    /// <summary>
    /// The following enum contains all the legal OpCode values used in Art-Net packets
    /// </summary>
    /// /// <remarks>
    /// See the ArtNet <see href="https://artisticlicence.com/WebSiteMaster/User%20Guides/art-net.pdf">User Guide</see> for more.
    /// </remarks>
    public enum OpCodes : ushort
    {
        /// <summary>
        /// This is not a packet
        /// </summary>
        OpNone = 0x000,
        /// <summary>
        /// This is an ArtPoll packet. 
        /// No other data is contained in this UDP packet.
        /// </summary>
        OpPoll = 0x2000,
        /// <summary>
        /// This is an ArtPollReply Packet. 
        /// It contains device status information.
        /// </summary>
        OpPollReply = 0x2100,
        /// <summary>
        /// Diagnostics and data logging packet.
        /// </summary>
        OpDiagData = 0x2300,
        /// <summary>
        /// Used to send text based parameter commands.
        /// </summary>
        OpCommand = 0x2400,
        /// <summary>
        /// This is an ArtDmx data packet. 
        /// It contains zero start code DMX512 information for a single Universe.
        /// </summary>
        /// <remarks>
        /// Alternative name is OpOutput.
        /// </remarks>
        OpDmx = 0x5000,
        /// <summary>
        /// This is an ArtNzs data packet. 
        /// It contains non-zero start code (except RDM) DMX512 information for a single Universe.
        /// </summary>
        OpNzs = 0x5100,
        /// <summary>
        /// This is an ArtSync data packet. 
        /// It is used to force synchronous transfer of ArtDmx packets to a node’s output.
        /// </summary>
        OpSync = 0x5200,
        /// <summary>
        /// This is an ArtAddress packet. 
        /// It contains remote programming information for a Node.
        /// </summary>
        OpAddress = 0x6000,
        /// <summary>
        /// This is an ArtInput packet. 
        /// It contains enable – disable data for DMX inputs.
        /// </summary>
        OpInput = 0x7000,
        /// <summary>
        /// This is an ArtTodRequest packet. 
        /// It is used to request a Table of Devices (ToD) for RDM discovery
        /// </summary>
        OpTodRequest = 0x8000,
        /// <summary>
        /// This is an ArtTodData packet. 
        /// It is used to send a Table of Devices (ToD) for RDM discovery.
        /// </summary>
        OpTodData = 0x8100,
        /// <summary>
        /// This is an ArtTodControl packet. 
        /// It is used to send RDM discovery control messages.
        /// </summary>
        OpTodControl = 0x8200,
        /// <summary>
        /// This is an ArtRdm packet. 
        /// It is used to send all non discovery RDM messages.
        /// </summary>
        OpRdm = 0x8300,
        /// <summary> 
        /// This is an ArtRdmSub packet. 
        /// It is used to send compressed, RDM Sub-Device data.</summary>
        OpRdmSub = 0x8400,
        /// <summary>
        /// This is an ArtVideoSetup packet. 
        /// It contains video screen setup information for nodes that implement the extended video features.
        /// </summary>
        OpVideoSetup = 0xa010,
        /// <summary>
        /// This is an ArtVideoPalette packet. 
        /// It contains colour palette setup information for nodes that implement the extended video features.
        /// </summary>
        OpVideoPalette = 0xa020,
        /// <summary>
        /// This is an ArtVideoData packet. 
        /// It contains display data for nodes that implement the extended video features
        /// </summary>
        OpVideoData = 0xa040,
        [Obsolete("This packet is deprecated.")]
        OpMacMaster = 0xf000,
        [Obsolete("This packet is deprecated.")]
        OpMacSlave = 0xf100,
        /// <summary>
        /// This is an ArtFirmwareMaster packet. 
        /// It is used to upload new firmware or firmware extensions to the Node.
        /// </summary>
        OpFirmwareMaster = 0xf200,
        /// <summary>
        /// This is an ArtFirmwareReply packet. 
        /// It is returned by the node to acknowledge receipt of an ArtFirmwareMaster packet or ArtFileTnMaster packet.
        /// </summary>
        OpFirmwareReply = 0xf300,
        /// <summary>
        /// Uploads user file to node.
        /// </summary>
        OpFileTnMaster = 0xf400,
        /// <summary>
        /// Downloads user file from node.
        /// </summary>
        OpFileFnMaster = 0xf500,
        /// <summary>
        /// Server to Node acknowledge for download packets.
        /// </summary>
        OpFileFnReply = 0xf600,
        /// <summary>
        /// This is an ArtIpProg packet. 
        /// It is used to reprogramme the IP address and Mask of the Node.
        /// </summary>
        OpIpProg = 0xf800,
        /// <summary>
        /// This is an ArtIpProgReply packet. 
        /// It is returned by the node to acknowledge receipt of an ArtIpProg packet.
        /// </summary>
        OpIpProgReply = 0xf900,
        /// <summary>
        /// This is an ArtMedia packet. 
        /// It is Unicast by a Media Server and acted upon by a Controller.
        /// </summary>
        OpMedia = 0x9000,
        ///<summary>
        ///This is an ArtMediaPatch packet. 
        ///It is Unicast by a Controller and acted upon by a Media Server.
        ///</summary>
        OpMediaPatch = 0x9100,
        /// <summary>
        /// This is an ArtMediaControl packet. 
        /// It is Unicast by a Controller and acted upon by a Media Server
        /// </summary>
        OpMediaControl = 0x9200,
        /// <summary>
        /// This is an ArtMediaControlReply packet. 
        /// It is Unicast by a Media Server and acted upon by a Controller.
        /// </summary>
        OpMediaContrlReply = 0x9300,
        ///<summary>
        /// This is an ArtMediaPatch packet. 
        /// It is Unicast by a Controller and acted upon by a Media Server.
        /// </summary>
        OpTimeCode = 0x9700,
        ///<summary>
        /// Used to synchronise real time date and clock.
        /// </summary>
        OpTimeSync = 0x9800,
        ///<summary>
        /// Used to send trigger macros.
        /// </summary>
        OpTrigger = 0x9900,
        ///<summary>
        /// Requests a node's file list.
        /// </summary>
        OpDirectory = 0x9a00,
        ///<summary>
        /// Replies to OpDirectory with file list
        /// </summary>
        OpDirectoryReply = 0x9b00,
        /// <summary>
        /// This is for custom ArtNetPackets that you an end user can implement themself
        /// </summary>
        OpCustom = 0xffff
    }
}
