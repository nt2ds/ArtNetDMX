using NoisyCowStudios.Bin2Object;

namespace ArtNet.IO
{
    public class ArtNetData
    {
        [SkipBin2Object]
        public byte[] Buffer = new byte[1500];
        [SkipBin2Object]
        public int BufferSize = 1500;
        [SkipBin2Object]
        public int DataLength = 0;

        public bool Valid
        {
            get { return DataLength > 12; }
        }

        public short OpCode
        {
            get
            {
                return (short)(Buffer[8] | Buffer[9] << 8);
            }
        }
    }
}