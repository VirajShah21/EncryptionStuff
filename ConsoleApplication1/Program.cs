using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static string sourceFile = "/Users/viraj/Desktop/HelloWorld.txt";
        private static string compressedFile = "/Users/viraj/Desktop/HelloWorld.txt.gz";
        private static string encryptedFile = "/Users/viraj/Desktop/HelloWorld.txt.gz.edx";
        private static string decryptedFile = "/Users/viraj/Desktop/Decrypted.txt.gz";
        private static string inflatedFile = "/Users/viraj/Desktop/Decrypted.txt";

        public static void Main(string[] args)
        {
            if (!File.Exists(sourceFile))
                throw new Exception("Source file does not exist");

            FileSystemService.Compress(sourceFile, compressedFile);
            FileSystemService.Encrypt(compressedFile, encryptedFile, new byte[32]);
            
            Console.WriteLine($"What would be uploaded to S3: {encryptedFile}");
            
            FileSystemService.Decrypt(encryptedFile, decryptedFile, new byte[32]);
            FileSystemService.Inflate(decryptedFile, inflatedFile);
            
            Console.WriteLine($"The final output: {inflatedFile} (What happens when the file is downloaded).");
        }
    }
}