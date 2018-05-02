using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataService
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Server waiting for requests:\n" +
                                  "Press <Spacebar> to generate test data.\n" +
                                  "Press <Enter> to send.\n" + 
                                  "Press <Esc> to quit.");

                var cki = new ConsoleKeyInfo();
                while (true)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Spacebar)
                    {
                        // Get sample wavelength data
                        Console.Write("Generating test data ... "); // DEBUG

                        //int samples = 40000;
                        //int wavelengths = 4200;
                        //int references = 3;

                        //var data = Data.GenerateBinaryDataBlob(samples, wavelengths, references);
                        //var success = Data.GenerateBinaryDataFile(@"dataset.dat", samples, wavelengths, references);
                        
                        Console.WriteLine("Done!"); // DEBUG
                    }

                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine("Received <Enter> => Uploading ...");
                        UploadDataAsync().GetAwaiter().GetResult();

                        Console.WriteLine("Done!"); // DEBUG
                    }

                    if (cki.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

            Console.WriteLine("Hello World!");
        }

        public static async Task UploadDataAsync()
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer container = null;
            
            string sourceFile = "dataset.dat";
            //string destinationFile = null;

            string connString = Environment.GetEnvironmentVariable("storageconnectionstring");

            if (CloudStorageAccount.TryParse(connString, out storageAccount))
            {
                // Get container ref
                CloudBlobClient client = storageAccount.CreateCloudBlobClient();
                container = client.GetContainerReference("test");

                // Get block blob and upload
                CloudBlockBlob blob = container.GetBlockBlobReference("dataset_40k_3ref");
                await blob.UploadFromFileAsync(sourceFile);

                //var hest = 5;
            }
            else {
                Console.WriteLine("Invalid connection string!");
            }
        }

    }

    // This should obviously not be here. It is used for testing
    public static class Data
    {
        public static byte[] GenerateBinaryDataBlob(int nSamples, int nPoints, int nRefs = 0)
        {
            var rng = new Random();

            var nValues = nPoints + nRefs;
            var data = new double[nValues];
            var bytes = new byte[nSamples * nValues * sizeof(double)];

            for (int i=0; i < nSamples; i++)
            {
                for (int j=0;j<nValues;j++) data[j] = rng.NextDouble();
            
                // Copy to byte[]
                Buffer.BlockCopy(data, 0, bytes, i*nValues, nValues);
            }

            return bytes;
        }

        public static bool GenerateBinaryDataFile(
            string filename, int nSamples, int nPoints, int nRefs = 0)
        {
            var rng = new Random();

            var nValues = nPoints + nRefs;
            var sample = new double[nValues];
            var bytes = new byte[nValues * sizeof(double)];

            var mode = FileMode.Create;
            var access = FileAccess.Write;

            using (FileStream fs = new FileStream(filename, mode, access))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                for (int i=0; i < nSamples; i++)
                {
                    for (int j=0;j<nValues;j++) sample[j] = rng.NextDouble();
            
                    // Copy to byte[]
                    Buffer.BlockCopy(sample, 0, bytes, 0, nValues);

                    // Write sample to file
                    writer.Write(bytes);
                }
            }

            return true;
        }
    }
}
