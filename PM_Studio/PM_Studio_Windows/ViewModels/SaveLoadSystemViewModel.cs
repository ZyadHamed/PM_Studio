using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    class SaveLoadSystemViewModel
    {
        #region Variables

        

        #endregion

        #region Methods
        public void Save<T>(string filePath, T objectToSave)
        {
            SaveLoadSystem.SaveData<T>(filePath, objectToSave);
        }

        public Algorithm ReturnAlgorithm(string filePath)
        {
            return SaveLoadSystem.LoadData<Algorithm>(filePath);
        }

        #endregion
    }
}
