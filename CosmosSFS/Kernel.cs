using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL.BlockDevice;
using SimpleFileSystem;
using Sys = Cosmos.System;

namespace CosmosSFS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();

            var blockDevice = BlockDevice.Devices[0];
            var p = new Partition(blockDevice, 0, blockDevice.BlockCount);
            var fs = new SimpleFS(new CosmosBlockDevice(p));

//            fs.Format();
//            Console.WriteLine("Loaded FS");
//
//            fs.WriteAllText("bob.txt", "A");
//            fs.WriteAllText("lol.txt", "B");

            fs.Load();
            Console.WriteLine("Loaded FS");

            Console.WriteLine("bob.txt");
            Console.WriteLine(fs.ReadAllText("bob.txt"));
            Console.WriteLine("--------------------------------");

            Console.WriteLine("lol.txt");
            Console.WriteLine(fs.ReadAllText("lol.txt"));
            Console.WriteLine("--------------------------------");

            Console.WriteLine("Done");
        }

        protected override void Run()
        {
        }
    }
}