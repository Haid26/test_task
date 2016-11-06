using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace test_task
{
    class Gzip
    {
        public static void Compress(String fileSource, String fileDestination, int buffsize)
        {
            using (var fsInput = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
            {
                using (var fsOutput = new FileStream(fileDestination, FileMode.Create, FileAccess.Write))
                {
                    using (var gzipStream = new GZipStream(fsOutput, CompressionMode.Compress))
                    {
                        var buffer = new Byte[buffsize];
                        int h;
                        while ((h = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            gzipStream.Write(buffer, 0, h);
                        }
                    }
                }
            }
        }

        public static void Decompress(String fileSource, String fileDestination, int buffsize)
        {
            using (var fsInput = new FileStream(fileSource, FileMode.Open, FileAccess.Read))
            {
                using (var fsOutput = new FileStream(fileDestination, FileMode.Create, FileAccess.Write))
                {
                    using (var gzipStream = new GZipStream(fsInput, CompressionMode.Decompress))
                    {
                        var buffer = new Byte[buffsize];
                        int h;
                        while ((h = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fsOutput.Write(buffer, 0, h);
                        }
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string OperationType = args[0];
            string SourcePath = args[1];
            string DestinationPath = args[2];

            //string compressed = Path.ChangeExtension(SourcePath, "gz");
            //string decompressed = SourcePath.Insert(SourcePath.Length - 4, "_");
            int buffsize = 16384;
            if (OperationType == "compress")
            {
                test_task.Gzip.Compress(SourcePath, DestinationPath, buffsize);
            }
            else
            {
                test_task.Gzip.Decompress(SourcePath, DestinationPath, buffsize);
            }
        }

       
    }

}
