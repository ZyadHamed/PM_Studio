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
        /// <param name="filePath"></param>
        /// <param name="ObjectToWrite"></param>
        /// <param name="Append"></param>
        public static void SaveData<T>(string filePath, T ObjectToWrite, bool Append = false)
        {

            FileStream stream = new FileStream(filePath, Append ? FileMode.Append : FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, ObjectToWrite);
            stream.Close();

        }

        /// <summary>
        /// Gets a filePath, and returns a Class that was stored in binary in that filePath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T LoadData<T>(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }

        #endregion

    }
}
