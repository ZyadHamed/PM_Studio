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

        public AlgorithmTabItem ReturnAlgorithm(string filePath)
        {
            //Get the algorithm file from the given path and return it as a Class again 
            var Algorithm =  SaveLoadSystem.LoadData<Algorithm>(filePath);
            //Construct an Algorithm Editing tab using the information inside that Algorithm class,(and remove any stars at the file name)
            return new AlgorithmTabItem(null, Algorithm.algorithm, Algorithm.algorithmFileName.Replace("*",""), filePath);
        }

       public StoryConcepts ReturnStoryConcepts(string filePath)
       {
            return SaveLoadSystem.LoadData<StoryConcepts>(filePath);
       }

        public StoryConceptsTabItem ReturnStoryConceptsTabItem(string filePath)
        {
            //Get the StoryConcepts file inside that given path
            StoryConcepts storyConcepts = ReturnStoryConcepts(filePath);
            //Construct a StoryConcepts TabItem using the information in that file
            return new StoryConceptsTabItem(null, filePath, storyConcepts);
        }
        
        #endregion
    }
}
