using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConverterApp.Models
{
    class Converter
    {
        public void ConvertText(string originalText, int[] encryptionCipher = null, int encryptionDepth = 1)
        {
            CipherEncrypted = DeepEncryptWithCipher(OriginalText, EncryptionCipher, EncryptionDepth);
        }

        public string OriginalText { get; internal set; }
        public int[] EncryptionCipher { get; } = new[] { 1, 1, 2, 3, 5, 8 }; //Fixed fibonacci sequence
        public int EncryptionDepth { get; } = 1;
        public string CipherEncrypted { get; internal set; }

        public static string StringToBinary(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text.ToCharArray())
            {
                //Convert the char to base 2 and pad the output with 0
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        public static string BinaryToString(string text)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < text.Length; i += 8)
            {
                bytes.Add(Convert.ToByte(text.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(bytes.ToArray());
        }

        public static string StringToHex2(string data)
        {
            StringBuilder strb = new StringBuilder();
            byte[] bytearray = Encoding.ASCII.GetBytes(data);
            foreach (byte bytepart in bytearray)
            {
                strb.Append(bytepart.ToString("X2"));
            }
            return strb.ToString().ToUpper();
        }

        public static string HexToString(string hexString)
        {
            if (hexString == null || (hexString.Length & 1) == 1)
            {
                throw new ArgumentException();
            }
            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }

        public static string StringToBase64(string data)
        {
            byte[] bytearray = Encoding.ASCII.GetBytes(data);
            string result = Convert.ToBase64String(bytearray);
            return result;
        }

        public static string Base64ToString(string base64String)
        {
            byte[] bytearray = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(bytearray))
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        public static string DeepEncryptWithCipher(string originalText, int[] encryptionCipher, int encryptionDepth)
        {
            string result = originalText;
            for (int depth = 0; depth < encryptionDepth; depth++)
            {
                //Apply Encryption Cipher on current value of result
                result = EncryptWithCipher(result, encryptionCipher);
            }
            return result;
        }

        public static string EncryptWithCipher(string text, int[] encryptionCipher)
        {
            if (encryptionCipher == null || encryptionCipher.Length == 0)
            {
                return text;
            }
            //Store the original string converted to bytes
            //Convert the text data to Unicode byte in order to handle non ASCII value character
            byte[] fullNameBytes = Encoding.Unicode.GetBytes(text);
            //Build byte array from the original byte array that will receive the encrypted values
            byte[] bytearrayresult = fullNameBytes;
            int encryptionCipherIndex = 0;
            //Apply Encryption Cipher
            for (int i = 0; i < fullNameBytes.Length; i++)
            {
                //Set the Cipher index
                encryptionCipherIndex = i;
                //We reset the current encryption position to 0 to restart at the beginning of the encryptionCipher
                if (encryptionCipherIndex >= encryptionCipher.Length)
                {
                    //Reset the cryper postion to zero and restart sequence
                    encryptionCipherIndex = 0;
                }
                if (fullNameBytes[i] != 0)
                {
                    bytearrayresult[i] = (byte)(fullNameBytes[i] + encryptionCipher[encryptionCipherIndex]);
                }
            }
            string newresult = Encoding.Unicode.GetString(bytearrayresult);
            return newresult;
        }


        public static string DeepDecryptWithCipher(string originalText, int[] encryptionCipher, int encryptionDepth)
        {
            string result = originalText;

            //For demonstration
            string[] encryptedValues = new string[encryptionDepth + 1];
            encryptedValues[0] = result;

            //Encrypt result encryptionDepth times
            for (int depth = 0; depth < encryptionDepth; depth++)
            {
                //Apply Encryption Cipher on current value of result
                result = DecryptWithCipher(result, encryptionCipher);

                //Add new encrypted result to the encrypted array fro demonstration
                encryptedValues[depth + 1] = result;
            }
            return result;
        }

        // Decrypts a cipher encrypted string
        public static string DecryptWithCipher(string text, int[] encryptionCipher)
        {
            //Convert the text data to Unicode byte in order to handle non ASCII value character
            byte[] fullNameBytes = Encoding.Unicode.GetBytes(text);
            //Build byte array from the original byte array that will receive the encrypted values
            byte[] bytearrayresult = fullNameBytes;

            int encryptionCipherIndex = 0;
            
            for (int i = 0; i < fullNameBytes.Length; i++)
            {
                //Set the Cipher index
                encryptionCipherIndex = i;

                //We reset the current encryption position to 0 to restart at the beginning of the encryptionCipher
                if (encryptionCipherIndex >= encryptionCipher.Length)
                {
                    //Reset the cryper postion to zero and restart sequence
                    encryptionCipherIndex = 0;
                }
                if (fullNameBytes[i] != 0)
                {
                    bytearrayresult[i] = (byte)(fullNameBytes[i] - encryptionCipher[encryptionCipherIndex]);
                }
            }

            string newresult = Encoding.Unicode.GetString(bytearrayresult);
            return newresult;
        }
    }
}