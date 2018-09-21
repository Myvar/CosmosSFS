using System.IO;
using SimpleFileSystem;

namespace WindowsDeviceBlockStream
{
    public class WindowsStreamBlockDevice : IBlockDevice
    {
        public WindowsStreamBlockDevice(Stream stream)
        {
            Stream = stream;
        }

        private Stream Stream { get; set; }

        public long BlockSize => 512;
        public long TotalBlocks => 65536;

        public byte[] ReadBlock(long offset)
        {
            Stream.Seek(offset * BlockSize, SeekOrigin.Begin);
            var buf = new byte[BlockSize];
            Stream.Read(buf, 0, (int) BlockSize);
            return buf;
        }

        public void WriteBlock(long offset, byte[] block)
        {
            Stream.Seek(offset * BlockSize, SeekOrigin.Begin);
            Stream.Write(block, 0, (int) BlockSize);
        }
    }
}