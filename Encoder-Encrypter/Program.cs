using ConverterApp.Models;
using System;
using System.Linq;

namespace ConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Text: ");
            string str = Console.ReadLine();   //User Input

            string binaryValue = Converter.StringToBinary(str);    //To Binary
            Console.WriteLine($"\nOriginal String: {str}\nBinary Convertion: {binaryValue}");

            string textFromBinary = Converter.BinaryToString(binaryValue);  //Back to ASCII
            Console.WriteLine($"Converting Binary back to text: {textFromBinary}");

            string hexaValue = Converter.StringToHex2(str);    //To Hexadecimal
            Console.WriteLine($"\nOriginal String: {str}\nHexadecimal Convertion: {hexaValue}");

            string textFromHex = Converter.HexToString(hexaValue);     //Back to ASCII
            Console.WriteLine($"Converting Hex back to string: {textFromHex}");

            string baseValue = Converter.StringToBase64(str);       //To Base64
            Console.WriteLine($"\nOriginal String: {str}\nBase64 Convertion: {baseValue}");

            string textFromBase = Converter.Base64ToString(baseValue);      // Back to ASCII
            Console.WriteLine($"Converting Base64 back to string: {textFromBase}");

            Console.WriteLine("\n______________________Deep Encrytion______________________");

            int[] cipher = new[] { 1, 1, 2, 3, 5, 8, 13 };      //Fixed fibonacci sequence
            string cipherString = String.Join(",", cipher.Select(x => x.ToString()));

            int encryptionDepth = 20;

            string nameDeepEncryptWithCipher = Converter.DeepEncryptWithCipher(str, cipher, encryptionDepth);
            Console.WriteLine($"Deep Encryption Depth: {encryptionDepth} | Cipher: {{{cipherString}}} {nameDeepEncryptWithCipher}");

            string nameDeepDecryptWithCipher = Converter.DeepDecryptWithCipher(nameDeepEncryptWithCipher, cipher, encryptionDepth);
            Console.WriteLine($"Deep Decryption Depth: {encryptionDepth} | Cipher: {{{cipherString}}} {nameDeepDecryptWithCipher}");
        }
    }
}