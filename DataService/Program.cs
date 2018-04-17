using System;
using System.IO;
using System.Collections.Generic;

namespace DataService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server waiting for requests. " +
                                  "Press <Spacebar> to generate test data. " +
                                  "Press <Enter> to send. " + 
                                  "Press <Esc> to quit.");

                var cki = new ConsoleKeyInfo();
                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Spacebar)
                    {
                        // Get sample wavelength data
                        Console.Write("Generating test data ... "); // DEBUG

                        int samples = 40000; 
                        int wavelengths = 4200;
                        int references = 3;

                        var data = Data.GenerateBinaryDataBlob(samples, wavelengths, references);
                        
                        Console.WriteLine("Done!"); // DEBUG
                    }

                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("Received <Enter> ... No action ...");
                    }

                    if (cki.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

            Console.WriteLine("Hello World!");
        }
    }

    // This should obviously not be here. It is used for testing
    public static class Data
    {
        public static byte[] GenerateBinaryDataBlob(int nSamples, int nPoints, int nRefs = 0)
        {
            var nValues = nSamples * (nPoints + nRefs);
            var data = new double[nValues];
            var bytes = new byte[nValues * sizeof(double)];

            var rng = new Random();
            for (int i=0; i < nValues; i++)
            {
                data[i] = rng.NextDouble();
                Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
            }

            return bytes;
        }

        public static bool GenerateBinaryDataFile(string filename, int nSamples, int nPoints)
        {
            var rng = new Random();
            var sample = new double[nPoints];

            var mode = FileMode.Create;
            var access = FileAccess.Write;

            using (FileStream fs = new FileStream(filename, mode, access))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                for (int i=0; i < nSamples; i++)
                {
                    // Generate next sample
                    for (int j = 0; j < nPoints; j++)
                    {
                        sample[j] = rng.NextDouble();
                    }

                    var bytes = new byte[sample.Length * sizeof(double)];
                    Buffer.BlockCopy(sample, 0, bytes, 0, bytes.Length);

                    // Write sample to file
                    writer.Write(bytes);
                }
            }

            return true;
        }
    }
}
