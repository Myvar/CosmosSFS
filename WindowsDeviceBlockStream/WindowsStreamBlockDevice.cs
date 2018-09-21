using System.IO;
using DiscUtils;
using SimpleFileSystem;

namespace WindowsDeviceBlockStream
{
    public class WindowsStreamBlockDevice : IBlockDevice
    {
        public WindowsStreamBlockDevice(string path)
        {
            long diskSize = 30 * 1024 * 1024; //30MB
            _disk = VirtualDisk.OpenDisk(path, FileAccess.ReadWrite);

            Stream = _disk.Content;
        }

        private Stream Stream { get; set; }
        private VirtualDisk _disk { get; set; }
        private long sectorSize = 0;
        private long blocks = 0;

        public long BlockSize => _disk.BlockSize;
        public long TotalBlocks => _disk.Capacity / _disk.BlockSize;

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