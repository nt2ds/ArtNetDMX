using System.Globalization;

namespace ArtNet.Rdm
{
    /// <summary>
    /// Device UId for RDM 
    /// </summary>
    public class UId
    {
        /// <summary>
        /// The manufacturer id of the device
        /// </summary>
        public ushort ManufacturerId;

        /// <summary>
        /// The id of the device
        /// </summary>
        public uint DeviceId;

        public UId(ushort manufacturerId, uint deviceId)
        {
            ManufacturerId = manufacturerId;
            DeviceId = deviceId;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", ManufacturerId.ToString("X4"), DeviceId.ToString("X8"));
        }


        public static UId Parse(string value)
        {
            string[] parts = value.Split(':');
            return new UId(ushort.Parse(parts[0], NumberStyles.HexNumber), uint.Parse(parts[1], NumberStyles.HexNumber));
        }

        public override int GetHashCode()
        {
            return ManufacturerId.GetHashCode() + DeviceId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            UId uid = obj as UId;
            return uid.ManufacturerId.Equals(ManufacturerId) && uid.DeviceId.Equals(DeviceId);
        }
    }
}