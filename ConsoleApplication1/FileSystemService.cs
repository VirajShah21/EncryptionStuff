using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication1
{
    public class FileSystemService
    {
        public static void Compress(Stream source, Stream destination)
        {
            using (var compressionStream = new GZipStream(destination, CompressionLevel.Optimal))
            {
                source.CopyTo(compressionStream);
            }
        }

        public static void Compress(string source, string destination)
        {
            Stream sourceStream = File.OpenRead(source);
            Stream destinationStream = File.OpenWrite(destination);
            Compress(sourceStream, destinationStream);
            sourceStream.Dispose();
            destinationStream.Dispose();
        }

        public static void Compress(Stream source, string destination)
        {
            Stream destinationStream = File.OpenWrite(destination);
            Compress(source, destinationStream);
            destinationStream.Dispose();
        }

        public static void Compress(string source, Stream destination)
        {
            Stream sourceStream = File.OpenRead(source);
            Compress(sourceStream, destination);
            sourceStream.Dispose();
        }

        public static void Inflate(Stream source, Stream destination)
        {
            using (GZipStream decompressionStream = new GZipStream(source, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(destination);
            }
        }

        public static void Inflate(string source, string destination)
        {
            Stream sourceStream = File.OpenRead(source);
            Stream destinationStream = File.OpenWrite(destination);
            Inflate(sourceStream, destinationStream);
            sourceStream.Dispose();
            destinationStream.Dispose();
        }

        public static void Inflate(Stream source, string destination)
        {
            Stream destinationStream = File.OpenWrite(destination);
            Inflate(source, destinationStream);
            destinationStream.Dispose();
        }

        public static void Inflate(string source, Stream destination)
        {
            Stream sourceStream = File.OpenRead(source);
            Inflate(sourceStream, destination);
            sourceStream.Dispose();
        }

        public static void Encrypt(Stream source, Stream destination, byte[] key)
        {
            // Create AES algorithm with the provided key
            var aes = new AesManaged
            {
                Key = key,
                IV = new byte[16], // initialization vector
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            // Create encryptor and stream to encrypt the data
            var encryptor = aes.CreateEncryptor();
            var cryptoStream = new CryptoStream(destination, encryptor, CryptoStreamMode.Write);

            // Read the input stream and encrypt its data, writing it to the output stream
            var buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                cryptoStream.Write(buffer, 0, bytesRead);
            }

            cryptoStream.FlushFinalBlock();
            destination.Flush();
            
            cryptoStream.Close();
        }

        public static void Encrypt(string source, string destination, byte[] key)
        {
            Stream sourceStream = File.OpenRead(source);
            Stream destinationStream = File.OpenWrite(destination);
            Encrypt(sourceStream, destinationStream, key);
            sourceStream.Dispose();
            destinationStream.Dispose();
        }

        public static void Encrypt(Stream source, string destination, byte[] key)
        {
            Stream destinationStream = File.OpenWrite(destination);
            Encrypt(source, destinationStream, key);
            destinationStream.Dispose();
        }

        public static void Encrypt(string source, Stream destination, byte[] key)
        {
            Stream sourceStream = File.OpenRead(source);
            Encrypt(sourceStream, destination, key);
            sourceStream.Dispose();
        }

        public static void Decrypt(Stream source, Stream destination, byte[] key)
        {
            // Create AES algorithm with the provided key
            var aes = new AesManaged
            {
                Key = key,
                IV = new byte[16], // initialization vector
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            // Create decryptor and stream to decrypt the data
            var decryptor = aes.CreateDecryptor();
            var cryptoStream = new CryptoStream(source, decryptor, CryptoStreamMode.Read);

            // Read the encrypted data from the input stream and decrypt it, writing the decrypted data to the output stream
            var buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
            }

            // Flush and close the streams
            destination.Flush();
            cryptoStream.Flush();

            cryptoStream.Close();
        }

        public static void Decrypt(string source, string destination, byte[] key)
        {
            Stream sourceStream = File.OpenRead(source);
            Stream destinationStream = File.OpenWrite(destination);
            Decrypt(sourceStream, destinationStream, key);
            sourceStream.Dispose();
            destinationStream.Dispose();
        }

        public static void Decrypt(Stream source, string destination, byte[] key)
        {
            Stream destinationStream = File.OpenWrite(destination);
            Decrypt(source, destinationStream, key);
            destinationStream.Dispose();
        }

        public static void Decrypt(string source, Stream destination, byte[] key)
        {
            Stream sourceStream = File.OpenRead(source);
            Decrypt(sourceStream, destination, key);
            sourceStream.Dispose();
        }

        private static bool ComputedHmacAreEqual(byte[] expectedHmac, byte[] computedHmac)
        {
            bool result = true;
            if (expectedHmac.Length != computedHmac.Length)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < expectedHmac.Length; i++)
                {
                    if (expectedHmac[i] != computedHmac[i])
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}