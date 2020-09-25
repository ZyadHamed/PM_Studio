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

        /// <summary>
        /// Gets all Algorithms inside a Given Path
        /// </summary>
        /// <param name="algorithmsFilePath">The filePath to search in</param>
        /// <returns>A List Containing all the Algorithms inside the Algorithm Files</returns>
        public List<Algorithm> GetAllAlgorithms(string algorithmsFilePath)
        {
            //Create a List of string that contains the FilePaths of all pmalg files
            List<string> algorithmFilePaths = FileManger.GetAllFilesByExtension(algorithmsFilePath, ".pmalg");
            
            //Create a List of Algorithms that will store the Algorithms inside the pmalg files
            List<Algorithm> algorithms = new List<Algorithm>();

            //Loop on all the file pathes
            foreach(string filePath in algorithmFilePaths)
            {
                //Add the algorithm inside the current file into the List of algorithms
                algorithms.Add(SaveLoadSystem.LoadData<Algorithm>(filePath));
            }
            
            //Return the algorithms list
            return algorithms;
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
        /// Returns a shedule in a shedule file in a given path
        /// </summary>
        /// <param name="filePath">The path of the shedule file</param>
        /// <returns></returns>
        public Shedule GetShedule(string filePath)
        {
            return SaveLoadSystem.LoadData<Shedule>(filePath);
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
                algorithmFileName = fileName + ".pmalg",
                algorithm = ""
            };
            //Save that algorithm in the given path
            Save(filePath + algorithm.algorithmFileName, algorithm);
        }

        public void CreateStoryConceptsFile(string filePath, string fileName)
        {
            StoryConcepts storyConcepts = new StoryConcepts()
            {
                fileName = fileName + ".pmstory",
                StoryTypes = new string[] { "" },
                StoryIdea = "",
                PlotTwists = "",
                PlotPoints = "",
                StoryEvents = ""
            };
            Save(filePath + storyConcepts.fileName, storyConcepts);
        }

        public void CreateShedule(string filePath, string SheduleName)
        {
            Shedule shedule = new Shedule()
            {
                Name = SheduleName + ".pmshed",
                Tasks = new List<Task>()
            };
            Save(filePath + shedule.Name, shedule);
        }

        #endregion
    }
}
