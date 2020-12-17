using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace PM_Studio
{
    public class SecurityManger
    {
        /// <summary>
        /// Combines byte arrays into one array
        /// </summary>
        /// <param name="byteArrays">the arrays to be combined</param>
        /// <returns></returns>
        public byte[] CombineByteArrays(params byte[][] byteArrays)
        {
            //Create an empty List of bytes to store the bytes of all arays
            List<byte> CombinedBytes = new List<byte>();
            //Loop on each byte array
            foreach(byte[] array in byteArrays)
            {
                //Loop on each byte in that array
                foreach(byte bytenumber in array)
                {
                    //Add that byte to the bytes list
                    CombinedBytes.Add(bytenumber);
                }
            }

            //return the List as a byte array
            return CombinedBytes.ToArray();
        }

        /// <summary>
        /// Splits a given byte array into subarrays by using given Split numbers
        /// </summary>
        /// <param name="array">the array which will be splitted into subarrays</param>
        /// <param name="SplitNumbers">The Indexes which we will create a new subarray from them
        /// for example if the given array was { 255, 64, 25, 79, 18, 10, 90, 121, 34, 56, 97, 56, 76 },
        /// and the given split numbers were 3,4,3
        /// the first sub array generated will contain the first 3 bytes in the array,
        /// and the second subarray will contain the next 4 numbers
        /// and the third subarray will contain the next 3 numbers, and so on.</param>
        /// <returns></returns>
        public List<byte[]> SplitByteArray(byte[] array, params int[] SplitNumbers)
        {
            //Create a CurrentArrayIndex variable which holds the current index of the array
            int CurrentArrayIndex = 0;

            //Create an empty List which will hold the resulted subarrays
            List<byte[]> byteArrays = new List<byte[]>();
            //Loop on each SplitNumber given
            foreach(int SplitNumber in SplitNumbers)
            {
                //Create an array which will hold a subarray of the given array
                byte[] subArray = new byte[SplitNumber];

                //Loop inside the main array
                for (int i = 0; i < SplitNumber; i++)
                {
                    //Add the items which corrspondes to the current index in the array to the subarray
                    subArray[i] = array[CurrentArrayIndex];
                    //Increase the CurrentArrayIndex variable by 1 to corrospond to the next value in the array
                    CurrentArrayIndex++;
                    
                }
                //Add the resulted subarray into the ByteArrayList
                byteArrays.Add(subArray);
            }

            //Return the Resulted ByteArrayList
            return byteArrays;
        }

        public byte[] GenerateRandomNumber(int length)
        {
            //Create a RNGCryptoServiceProvider to generate the Random Number
            using (RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                //Create an empty byte array to store the RandomNumber
                byte[] randomNumber = new byte[length];

                //Fill that array with some random values and return it
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        /// <summary>
        /// Encrypts a file in a given path using the AES-GCM encrption
        /// </summary>
        /// <param name="filePath">The file to be encrypted</param>
        /// <returns></returns>
        public (byte[] cipherText, byte[] tag) EncryptFile(string filePath)
        {
            //Generate a 32 byte array to create the encryption key
            byte[] Key = GenerateRandomNumber(32);
            
            //Generate a 12 byte array nonce that will to create the IV of the encryption
            byte[] nonce = GenerateRandomNumber(12);

            //Generate a 16 byte array to recive the generated authentication tag
            byte[] tag = new byte[16];

            //Get the length of the data inside the required file as a byte array and create an empty byte array with that length
            //(as the encrypted data will have the same bytes length as the original data), this array will store the encrypted text
            byte[] chipherText = new byte[File.ReadAllBytes(filePath).Length];

            //Intialize the AES GCM encryption class and encrypt the data in the file and store it in the cipherText variable
            AesGcm aes = new AesGcm(Key);
            aes.Encrypt(nonce, File.ReadAllBytes(filePath), chipherText, tag);

            //Combine the Key, nonce, tag, and the cipherText in one byte array(to make restoring them for the decryption easier)
            //and store that array as a Base64 string in the required file
            File.WriteAllText(filePath, Convert.ToBase64String(CombineByteArrays(Key, nonce, tag, chipherText)));
            return (chipherText, tag);
        }

        /// <summary>
        /// Decrypts a  encrypted with AES-GCM in a given path
        /// </summary>
        /// <param name="filePath">the file to be decrypted</param>
        public void DecryptFile(string filePath)
        {
            //Convert the file text into a byte array which contains all the data of the previous encryption
            byte[] FullData = Convert.FromBase64String(File.ReadAllText(filePath));

            //Create a List of byte arrays which holds each byte array stored in the encryption
            //The first 32 bytes is the key, the next 12 bytes is the nonce, the next 16 bytes is the tag, and the remaining bytes are the original content
            //(the byte array length - 60 is simply the whole byte array without the length of the first 3 arrays)
            List<byte[]> DataList = SplitByteArray(FullData, 32, 12, 16, FullData.Length - 60);

            //Get the Key, nonce, tag, and cipherText from the DataList
            byte[] Key = DataList[0];
            byte[] nonce = DataList[1];
            byte[] tag = DataList[2];
            byte[] cipherText = DataList[3];

            //Create an Empty byte array which will store the resulted decrypted text with the same length of the encrypted one
            byte[] PlainText = new byte[cipherText.Length];

            //Intialize the AES GCM encryption class and decrypt the data in the file and store it in the PlainText variable
            AesGcm aes = new AesGcm(Key);
            aes.Decrypt(nonce, cipherText, tag, PlainText);

            //Store the resulted decrypted array in the file
            File.WriteAllBytes(filePath, PlainText);
        }
    }
}
