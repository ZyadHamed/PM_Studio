using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PM_Studio
{
    public class SaveLoadSystem
    {

        #region Methods

        /// <summary>
        /// Gets a Serilizable Class, Then Save it in a given location in the form of a binary file
        /// </summary>
        /// <param name="filePath">The file path to write the Class in it </param>
        /// <param name="ObjectToWrite">The Class to be serilized</param>
        /// <param name="Append">the bool which indicates whether the given object is appended to the file or overrites it</param>
        public static void SaveData<T>(string filePath, T ObjectToWrite, bool Append = false)
        {
            //Intialize a SecurityManger Class which will be responsible for decryption of data
            SecurityManger securityManger = new SecurityManger();

            //Serilize the given object as binary data in the class
            FileStream stream = new FileStream(filePath, Append ? FileMode.Append : FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, ObjectToWrite);
            stream.Close();

            //Encrypt the resulted data for more security
            securityManger.EncryptFile(filePath);
        }

        /// <summary>
        /// Gets a filePath, and returns a Class that was stored in binary in that filePath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>The class stored within that file</returns>
        public static T LoadData<T>(string filePath)
        {
            //Intialize a SecurityManger Class which will be responsible for decryption of data
            SecurityManger securityManger = new SecurityManger();

            //Decrypt the text in the file
            securityManger.DecryptFile(filePath);

            //Return the binary data in that file as a class
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion

    }
}
