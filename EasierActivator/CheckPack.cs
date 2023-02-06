using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace EasierActivator
{
    class RC2Crypt
    {
        public void Sample()
        {
            byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes("Here is some data.");

            //Create a new RC2CryptoServiceProvider.
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.UseSalt = true;

            rc2CSP.GenerateKey();
            rc2CSP.GenerateIV();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, rc2CSP.CreateEncryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(originalBytes, 0, originalBytes.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            byte[] encryptedBytes = msEncrypt.ToArray();

            //Decrypt the previously encrypted message.
            MemoryStream msDecrypt = new MemoryStream(encryptedBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rc2CSP.CreateDecryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Read);

            byte[] unencryptedBytes = new byte[originalBytes.Length];

            //Read the data out of the crypto stream.
            csDecrypt.Read(unencryptedBytes, 0, unencryptedBytes.Length);

            //Convert the byte array back into a string.
            string plaintext = ASCIIEncoding.ASCII.GetString(unencryptedBytes);

            //Display the results.
            System.Windows.Forms.MessageBox.Show("Unencrypted text: " + plaintext);

            //   Console.ReadLine();

        }

        public String Decrypt(String text)
        {
            // byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes(text);

            //Create a new RC2CryptoServiceProvider.
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            byte[] encryptedBytes = GetBytes(text);

            //Decrypt the previously encrypted message.
            MemoryStream msDecrypt = new MemoryStream(encryptedBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rc2CSP.CreateDecryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Read);

            byte[] unencryptedBytes = new byte[encryptedBytes.Length];

            //Read the data out of the crypto stream.
            csDecrypt.Read(unencryptedBytes, 0, unencryptedBytes.Length);

            //Convert the byte array back into a string.
            string plaintext = ASCIIEncoding.ASCII.GetString(unencryptedBytes);

            //Display the results.
            //System.Windows.Forms.MessageBox.Show("Unencrypted text: " + plaintext);
            return plaintext;

            //   Console.ReadLine();

        }

        public String Encrypt(String text)
        {
            byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes(text);

            //Create a new RC2CryptoServiceProvider.
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.UseSalt = true;

            rc2CSP.GenerateKey();
            rc2CSP.GenerateIV();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, rc2CSP.CreateEncryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(originalBytes, 0, originalBytes.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            byte[] encryptedBytes = msEncrypt.ToArray();

            return GetString(encryptedBytes);

        }

        private String GetString(byte[] sourceBytes)
        {
            String rst = "";

            for (int w = 0; w < sourceBytes.Length; w++)
            {
                rst = rst.Insert(rst.Length, System.Convert.ToString(sourceBytes[w]));
                if (w != sourceBytes.Length - 1)
                {
                    rst = rst.Insert(rst.Length, "-");
                }
            }

            return rst;
        }

        private byte[] GetBytes(String sourceString)
        {


            String[] aux = sourceString.Split('-');
            byte[] rstBytes = new byte[aux.Length];

            for (int w = 0; w < aux.Length; w++)
            {
                rstBytes[w] = System.Convert.ToByte(aux[w]);

            }

            return rstBytes;
        }
    }
    class CheckPack
    {
        private static DESCryptoServiceProvider des;
        public CheckPack()
        {
            des = new DESCryptoServiceProvider();

        }

        public String GetDesKey()
        {
            return Encoding.ASCII.GetString(des.Key);
        }
        public String GetDesIV()
        {
            return Encoding.ASCII.GetString(des.IV);
        }

        public void SetDesKey(String key)
        {
            des.Key =  Encoding.ASCII.GetBytes(key);
        }
        public void SetDesIV(String iv)
        {
            des.IV = Encoding.ASCII.GetBytes(iv);
        }
        public String GetEncryption(String msg)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);
             //byte[] encodedBytes = EncodeBytes(sourceBytes);
          
             byte[] encodedBytes = EncodeBytes(Encoding.ASCII.GetBytes(msg));
            
          return   Encoding.ASCII.GetString(encodedBytes);

        }
        public String GetEncryption2(String msg)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);
            //byte[] encodedBytes = EncodeBytes(sourceBytes);

            byte[] encodedBytes = EncodeBytes2(Encoding.ASCII.GetBytes(msg));

            return Encoding.ASCII.GetString(encodedBytes);

        }
        public String GetEncryption3(String msg)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);
            //byte[] encodedBytes = EncodeBytes(sourceBytes);

            byte[] encodedBytes = EncodeBytes2(Encoding.ASCII.GetBytes(msg));

            return GetString(encodedBytes);

        }

        public String GetDecryption(String msg)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);

            byte[] encodedBytes = Encoding.ASCII.GetBytes(msg);

            byte[] decodedBytes = DecodeBytes(encodedBytes);


            return Encoding.ASCII.GetString(decodedBytes);

        }

        public String GetDecryption2(String msg, String key, String iv)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);

            byte[] encodedBytes = Encoding.ASCII.GetBytes(msg);

            byte[] decodedBytes = DecodeBytes2(encodedBytes, Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(iv));


            return Encoding.ASCII.GetString(decodedBytes);

        }
        public String GetBoth(String msg)
        {

            // byte[] sourceBytes = Encoding.ASCII.GetBytes(message);
            //byte[] encodedBytes = EncodeBytes(sourceBytes);

            byte[] encodedBytes = EncodeBytes(Encoding.ASCII.GetBytes(msg));

            msg = "";
            for (int w = 0; w < encodedBytes.Length; w++)
            {
                msg= msg.Insert(msg.Length, System.Convert.ToString(encodedBytes[w]));
            }
          //  msg = Encoding.ASCII.GetString(encodedBytes);

            byte[] bytearrayinput = new byte[msg.Length];
            //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            for (int w = 0; w < msg.Length; w++)
            {
                bytearrayinput[w] = System.Convert.ToByte(msg[w]);
            }

            //encodedBytes = Encoding.ASCII.GetBytes(msg);

            byte[] decodedBytes = DecodeBytes(bytearrayinput);


            return Encoding.ASCII.GetString(decodedBytes);

        }
        private String GetString(byte[] sourceBytes)
        {
            String rst = "";

            for (int w = 0; w < sourceBytes.Length; w++)
            {
                rst = rst.Insert(rst.Length, System.Convert.ToString(sourceBytes[w]));
                if (w != sourceBytes.Length - 1)
                {
                    rst = rst.Insert(rst.Length, "-");
                }
            }

            return rst;
        }

        private byte[] GetBytes(String sourceString)
        {


            String[] aux = sourceString.Split('-');
            byte[] rstBytes = new byte[aux.Length];

            for (int w = 0; w < aux.Length; w++)
            {
                rstBytes[w] = System.Convert.ToByte(aux[w]);

            }

            return rstBytes;
        }
        private static byte[] EncodeBytes(byte[] sourceBytes)
        {
            int currentPosition = 0;
            byte[] targetBytes = new byte[1024];
            int sourceByteLength = sourceBytes.Length;

            // Create a DES encryptor from this instance to perform encryption.
            CryptoAPITransform cryptoTransform =
                (CryptoAPITransform)des.CreateEncryptor();



            // Retrieve the block size to read the bytes.
            int inputBlockSize = cryptoTransform.InputBlockSize;

            // Retrieve the key handle.
            IntPtr keyHandle = cryptoTransform.KeyHandle;

            // Retrieve the block size to write the bytes.
            int outputBlockSize = cryptoTransform.OutputBlockSize;

            
            try
            {
                // Determine if multiple blocks can be transformed.
                if (cryptoTransform.CanTransformMultipleBlocks)
                {
                    int numBytesRead = 0;
                    while (sourceByteLength - currentPosition >= inputBlockSize)
                    {
                        // Transform the bytes from currentPosition in the
                        // sourceBytes array, writing the bytes to the targetBytes
                        // array.
                        numBytesRead = cryptoTransform.TransformBlock(
                            sourceBytes,
                            currentPosition,
                            inputBlockSize,
                            targetBytes,
                            currentPosition);

                        // Advance the current position in the sourceBytes array.
                        currentPosition += numBytesRead;
                    }

                    // Transform the final block of bytes.
                    byte[] finalBytes = cryptoTransform.TransformFinalBlock(
                        sourceBytes,
                        currentPosition,
                        sourceByteLength - currentPosition);

                    // Copy the contents of the finalBytes array to the
                    // targetBytes array.
                    finalBytes.CopyTo(targetBytes, currentPosition);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Caught unexpected exception:" + ex.ToString());
            }

            // Determine if the current transform can be reused.
            if (!cryptoTransform.CanReuseTransform)
            {
                // Free up any used resources.
                cryptoTransform.Clear();
            }

            // Trim the extra bytes in the array that were not used.
            return TrimArray(targetBytes);
        }
        private static byte[] EncodeBytes2(byte[] sourceBytes)
        {
            int currentPosition = 0;
            byte[] targetBytes = new byte[1024];
            int sourceByteLength = sourceBytes.Length;

            // Create a DES encryptor from this instance to perform encryption.
            CryptoAPITransform cryptoTransform =
                (CryptoAPITransform)des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes("WILDWEST"), ASCIIEncoding.ASCII.GetBytes("WILDWEST"));

            des.Key = ASCIIEncoding.ASCII.GetBytes("WILDWEST");
            des.IV = ASCIIEncoding.ASCII.GetBytes("WILDWEST");

            // Retrieve the block size to read the bytes.
            int inputBlockSize = cryptoTransform.InputBlockSize;

            // Retrieve the key handle.
            IntPtr keyHandle = cryptoTransform.KeyHandle;

            // Retrieve the block size to write the bytes.
            int outputBlockSize = cryptoTransform.OutputBlockSize;


            try
            {
                // Determine if multiple blocks can be transformed.
                if (cryptoTransform.CanTransformMultipleBlocks)
                {
                    int numBytesRead = 0;
                    while (sourceByteLength - currentPosition >= inputBlockSize)
                    {
                        // Transform the bytes from currentPosition in the
                        // sourceBytes array, writing the bytes to the targetBytes
                        // array.
                        numBytesRead = cryptoTransform.TransformBlock(
                            sourceBytes,
                            currentPosition,
                            inputBlockSize,
                            targetBytes,
                            currentPosition);

                        // Advance the current position in the sourceBytes array.
                        currentPosition += numBytesRead;
                    }

                    // Transform the final block of bytes.
                    byte[] finalBytes = cryptoTransform.TransformFinalBlock(
                        sourceBytes,
                        currentPosition,
                        sourceByteLength - currentPosition);

                    // Copy the contents of the finalBytes array to the
                    // targetBytes array.
                    finalBytes.CopyTo(targetBytes, currentPosition);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Caught unexpected exception:" + ex.ToString());
            }

            // Determine if the current transform can be reused.
            if (!cryptoTransform.CanReuseTransform)
            {
                // Free up any used resources.
                cryptoTransform.Clear();
            }

            // Trim the extra bytes in the array that were not used.
            return TrimArray(targetBytes);
        }

        // Decode the specified byte array using CryptoAPITranform.
        private static byte[] DecodeBytes(byte[] sourceBytes)
        {
            byte[] targetBytes = new byte[1024];
            int currentPosition = 0;

            // Create a DES decryptor from this instance to perform decryption.
            CryptoAPITransform cryptoTransform =
                (CryptoAPITransform)des.CreateDecryptor();

            int inputBlockSize = cryptoTransform.InputBlockSize;
            int sourceByteLength = sourceBytes.Length;

            try
            {
                int numBytesRead = 0;
                while (sourceByteLength - currentPosition >= inputBlockSize)
                {
                    // Transform the bytes from current position in the 
                    // sourceBytes array, writing the bytes to the targetBytes
                    // array.
                    numBytesRead = cryptoTransform.TransformBlock(
                        sourceBytes,
                        currentPosition,
                        inputBlockSize,
                        targetBytes,
                        currentPosition);

                    // Advance the current position in the source array.
                    currentPosition += numBytesRead;
                }

                // Transform the final block of bytes.
                byte[] finalBytes = cryptoTransform.TransformFinalBlock(
                    sourceBytes,
                    currentPosition,
                    sourceByteLength - currentPosition);

                // Copy the contents of the finalBytes array to the targetBytes
                // array.
                finalBytes.CopyTo(targetBytes, currentPosition);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Caught unexpected exception:" + ex.ToString());
            }

            // Strip out the second block of bytes.
            Array.Copy(targetBytes, (inputBlockSize * 2), targetBytes, inputBlockSize, targetBytes.Length - (inputBlockSize * 2));

            // Trim the extra bytes in the array that were not used.
            return TrimArray(targetBytes);
        }


        private static byte[] DecodeBytes2(byte[] sourceBytes, byte[] key, byte[] iv)
        {
            byte[] targetBytes = new byte[1024];
            int currentPosition = 0;

            // Create a DES decryptor from this instance to perform decryption.
            CryptoAPITransform cryptoTransform =
                (CryptoAPITransform)des.CreateDecryptor();
            des.Key = key;
            des.IV = iv;

            int inputBlockSize = cryptoTransform.InputBlockSize;
            int sourceByteLength = sourceBytes.Length;

            try
            {
                int numBytesRead = 0;
                while (sourceByteLength - currentPosition >= inputBlockSize)
                {
                    // Transform the bytes from current position in the 
                    // sourceBytes array, writing the bytes to the targetBytes
                    // array.
                    numBytesRead = cryptoTransform.TransformBlock(
                        sourceBytes,
                        currentPosition,
                        inputBlockSize,
                        targetBytes,
                        currentPosition);

                    // Advance the current position in the source array.
                    currentPosition += numBytesRead;
                }

                // Transform the final block of bytes.
                byte[] finalBytes = cryptoTransform.TransformFinalBlock(
                    sourceBytes,
                    currentPosition,
                    sourceByteLength - currentPosition);

                // Copy the contents of the finalBytes array to the targetBytes
                // array.
                finalBytes.CopyTo(targetBytes, currentPosition);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Caught unexpected exception:" + ex.ToString());
            }

            // Strip out the second block of bytes.
            Array.Copy(targetBytes, (inputBlockSize * 2), targetBytes, inputBlockSize, targetBytes.Length - (inputBlockSize * 2));

            // Trim the extra bytes in the array that were not used.
            return TrimArray(targetBytes);
        }

        // Resize the dimensions of the array to a size that contains only valid
        // bytes.
        private static byte[] TrimArray(byte[] targetArray)
        {
            System.Collections.IEnumerator enum1 = targetArray.GetEnumerator();
            int i = 0;

            while (enum1.MoveNext())
            {
                if (enum1.Current.ToString().Equals("0"))
                {
                    break;
                }
                i++;
            }

            // Create a new array with the number of valid bytes.
            byte[] returnedArray = new byte[i];
            for (int j = 0; j < i; j++)
            {
                returnedArray[j] = targetArray[j];
            }

            return returnedArray;
        }
    }

  

    class Encripter
    {
       public Encripter()
        {

        }
        //  Call this function to remove the key from memory after use for security
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 64 bits Key.
        public string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        public void EncryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        public void DecryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        public String EncryptText(string text,
         string sKey)
        {
            //FileStream fsInput = new FileStream(sInputFilename,
            //   FileMode.Open,
            //   FileAccess.Read);

               FileStream fsEncrypted = new FileStream("temp.tmp",
               FileMode.Create,
               FileAccess.Write);
           
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
           
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = ASCIIEncoding.ASCII.GetBytes(text);

          //  byte[] bytearrayinput = new byte[text.Length];
            //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            //for (int w = 0; w < text.Length; w++ )
            //{
            //    bytearrayinput[w] = System.Convert.ToByte(text[w]);
            //}
           // bytearrayinput = System.Convert.ToByte(text);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            
           // fsEncrypted.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
           // fsInput.Close();
            fsEncrypted.Close();

            StreamReader sr = new StreamReader("temp.tmp",Encoding.Unicode);
            string rst = sr.ReadToEnd();
            sr.Close();
            //FileInfo fi = new FileInfo("temp.tmp");
            //fi.Delete();

            return rst;
        }


        public string DecryptFile2(string sInputFilename,
         string sKey)
        {
            if (sKey.Length > 8)
            {
                sKey = sKey.Remove(8);
            }
            if (sKey.Length < 8)
            {
                while (sKey.Length < 8)
                    sKey = sKey + "W";

            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
           // fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            //fsDecrypted.Flush();
           // fsDecrypted.Close();
            StreamReader sr = new StreamReader(cryptostreamDecr);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
        }


        public GCHandle PinKey(string sSecretKey)
        {
            return GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
        }

        public void RemoveKey(GCHandle gch, int length)
        {
            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), length);
            gch.Free();
        }
        //static void Main()
        //{

        //}
    }
}
//namespace CSEncryptDecrypt
//{
//    class Class1
//    {
//        //  Call this function to remove the key from memory after use for security
//        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
//        public static extern bool ZeroMemory(IntPtr Destination, int Length);

//        // Function to Generate a 64 bits Key.
//        static string GenerateKey()
//        {
//            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
//            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

//            // Use the Automatically generated key for Encryption. 
//            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
//        }

//        static void EncryptFile(string sInputFilename,
//           string sOutputFilename,
//           string sKey)
//        {
//            FileStream fsInput = new FileStream(sInputFilename,
//               FileMode.Open,
//               FileAccess.Read);

//            FileStream fsEncrypted = new FileStream(sOutputFilename,
//               FileMode.Create,
//               FileAccess.Write);
//            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
//            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
//            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
//            ICryptoTransform desencrypt = DES.CreateEncryptor();
//            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
//               desencrypt,
//               CryptoStreamMode.Write);

//            byte[] bytearrayinput = new byte[fsInput.Length];
//            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
//            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
//            cryptostream.Close();
//            fsInput.Close();
//            fsEncrypted.Close();
//        }

//        static void DecryptFile(string sInputFilename,
//           string sOutputFilename,
//           string sKey)
//        {
//            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
//            //A 64 bit key and IV is required for this provider.
//            //Set secret key For DES algorithm.
//            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
//            //Set initialization vector.
//            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

//            //Create a file stream to read the encrypted file back.
//            FileStream fsread = new FileStream(sInputFilename,
//               FileMode.Open,
//               FileAccess.Read);
//            //Create a DES decryptor from the DES instance.
//            ICryptoTransform desdecrypt = DES.CreateDecryptor();
//            //Create crypto stream set to read and do a 
//            //DES decryption transform on incoming bytes.
//            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
//               desdecrypt,
//               CryptoStreamMode.Read);
//            //Print the contents of the decrypted file.
//            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
//            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
//            fsDecrypted.Flush();
//            fsDecrypted.Close();
//        }

//        static void Main()
//        {
//            // Must be 64 bits, 8 bytes.
//            // Distribute this key to the user who will decrypt this file.
//            string sSecretKey;

//            // Get the Key for the file to Encrypt.
//            sSecretKey = GenerateKey();

//            // For additional security Pin the key.
//            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);

//            // Encrypt the file.        
//            EncryptFile(@"C:\MyData.txt",
//               @"C:\Encrypted.txt",
//               sSecretKey);

//            // Decrypt the file.
//            DecryptFile(@"C:\Encrypted.txt",
//               @"C:\Decrypted.txt",
//               sSecretKey);

//            // Remove the Key from memory. 
//            ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
//            gch.Free();
//        }
//    }
//}