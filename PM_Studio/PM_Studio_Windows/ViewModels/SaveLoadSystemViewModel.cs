using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    class SaveLoadSystemViewModel
    {
        #region Variables

        TabControl tbFiles;

        #endregion

        #region Constructor
        public SaveLoadSystemViewModel(TabControl _tbFiles)
        {
            tbFiles = _tbFiles;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Saves a File in a Given Path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">The File path to save the file in</param>
        /// <param name="objectToSave">The Object to Save in that path</param>
        public void Save<T>(string filePath, T objectToSave)
        {
            SaveLoadSystem.SaveData<T>(filePath, objectToSave);
        }

        /// <summary>
        /// Returns an Algorithm Tab Item Based on existing Algorithm File in a Specific path
        /// </summary>
        /// <param name="algorithmFilePath">The File Path of the Algorithm</param>
        /// <returns></returns>
        public AlgorithmTabItem ReturnAlgorithm(string algorithmFilePath)
        {
            //Get the algorithm file from the given path and return it as a Class again 
            var Algorithm = SaveLoadSystem.LoadData<Algorithm>(algorithmFilePath);
            //Construct an Algorithm Editing tab using the information inside that Algorithm class,(and remove any stars at the file name)
            return new AlgorithmTabItem(tbFiles, Algorithm, algorithmFilePath);
        }


        public StoryConcepts ReturnStoryConcepts(string filePath)
        {
            return SaveLoadSystem.LoadData<StoryConcepts>(filePath);
        }

        /// <summary>
        /// Returns a StoryConcepts Tab Item Based on existing StoryConcepts File in a Specific path
        /// </summary>
        /// <param name="storyConceptsFilePath">The File Path of the StoryConcepts</param>
        /// <returns></returns>
        public StoryConceptsTabItem ReturnStoryConceptsTabItem(string storyConceptsFilePath)
        {
            //Get the StoryConcepts file inside that given path
            StoryConcepts storyConcepts = ReturnStoryConcepts(storyConceptsFilePath);
            //Construct a StoryConcepts TabItem using the information in that file
            return new StoryConceptsTabItem(tbFiles, storyConceptsFilePath, storyConcepts);
        }

        /// <summary>
        /// Returns a Team Based on a team file in a given Path
        /// </summary>
        /// <param name="filePath">The path of the team file</param>
        /// <returns></returns>
        public Team GetTeam(string filePath)
        {
            return SaveLoadSystem.LoadData<Team>(filePath);
        }

        /// <summary>
        /// Creates a Black Algorithm File
        /// </summary>
        /// <param name="filePath">The Path to Create the Algorthim file in</param>
        /// <param name="fileName">The Name of the Algorithm File</param>
        public void CreateAlgorithmFile(string filePath, string fileName)
        {
            //Create an Algorithm With the given name and with an empty Algorithm
            Algorithm algorithm = new Algorithm()
            {
                algorithmFileName = fileName + ".algorithm",
                algorithm = ""
            };
            //Save that algorithm in the given path
            Save(filePath + algorithm.algorithmFileName, algorithm);
        }

        #endregion
    }
}
