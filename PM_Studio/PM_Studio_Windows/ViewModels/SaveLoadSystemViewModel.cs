using System.Collections.Generic;
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

        #region TabItems Creation Methods

        /// <summary>
        /// Returns an Algorithm Tab Item Based on existing Algorithm File in a Specific path
        /// </summary>
        /// <param name="algorithmFilePath">The File Path of the Algorithm</param>
        /// <returns>an AlgorithmTabItem with the Data in that Algorithm file</returns>
        public AlgorithmTabItem ReturnAlgorithmTabItem(string algorithmFilePath)
        {
            //Get the algorithm Class from the file inside that given path
            var Algorithm = SaveLoadSystem.LoadData<Algorithm>(algorithmFilePath);

            //Construct an AlgorithmTabItem using the Data inside that Algorithm class
            return new AlgorithmTabItem(tbFiles, Algorithm, algorithmFilePath);
        }

        /// <summary>
        /// Returns a StoryConcepts Tab Item Based on existing StoryConcepts File in a Specific path
        /// </summary>
        /// <param name="storyConceptsFilePath">The File Path of the StoryConcepts</param>
        /// <returns>A StoryConceptsTabItem with the Data in that StoryConcepts file</returns>
        public StoryConceptsTabItem GetStoryConceptsTabItem(string storyConceptsFilePath)
        {
            //Get the StoryConcepts Class from the file inside that given path
            StoryConcepts storyConcepts = GetStoryConcepts(storyConceptsFilePath);

            //Construct a StoryConceptsTabItem using the Data in that resulting Class
            return new StoryConceptsTabItem(tbFiles, storyConceptsFilePath, storyConcepts);
        }

        /// <summary>
        /// Returns a NodeEditor Tab Item Based on existing Node System File in a Specific path
        /// </summary>
        /// <param name="nodeSystemFilePath">The File Path of the NodeSystem</param>
        /// <returns>A NodeEditorTabItem with the Data in that NodeSystem file</returns>
        public NodeEditorTabItem GetNodeEditorTabItem(string nodeSystemFilePath)
        {
            //Get the NodeSystem Class from the file inside that given path
            NodeSystem nodeSystem = SaveLoadSystem.LoadData<NodeSystem>(nodeSystemFilePath);

            //Construct a NodeEditorTabItem using the Data in that resulting Class
            return new NodeEditorTabItem(tbFiles, nodeSystemFilePath, nodeSystem);
        }


        #endregion

        #region File Getting Methods

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
            foreach (string filePath in algorithmFilePaths)
            {
                //Add the algorithm inside the current file into the List of algorithms
                algorithms.Add(SaveLoadSystem.LoadData<Algorithm>(filePath));
            }

            //Return the algorithms list
            return algorithms;
        }

        public StoryConcepts GetStoryConcepts(string storyConceptsFilePath)
        {
            return SaveLoadSystem.LoadData<StoryConcepts>(storyConceptsFilePath);
        }

        /// <summary>
        /// Returns a Team Based on a team file in a given Path
        /// </summary>
        /// <param name="teamFilePath">The path of the team file</param>
        /// <returns></returns>
        public Team GetTeam(string teamFilePath)
        {
            return SaveLoadSystem.LoadData<Team>(teamFilePath);
        }

        /// <summary>
        /// Returns a shedule in a shedule file in a given path
        /// </summary>
        /// <param name="sheduleFilePath">The path of the shedule file</param>
        /// <returns>a Shedule Class Containing the Data Stored in That File</returns>
        public Shedule GetShedule(string sheduleFilePath)
        {
            return SaveLoadSystem.LoadData<Shedule>(sheduleFilePath);
        }

        /// <summary>
        /// Returns the stages in a stages file in a given path
        /// </summary>
        /// <param name="stagesFilePath">the stages filepath</param>
        /// <returns></returns>
        public List<Stage> GetStages(string stagesFilePath)
        {
            return SaveLoadSystem.LoadData<List<Stage>>(stagesFilePath);
        }

        #endregion

        #region File Generating Methods

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
        /// Creates a Black Algorithm File
        /// </summary>
        /// <param name="filePath">The Path to Create the Algorthim file in</param>
        /// <param name="fileName">The Name of the Algorithm File</param>
        public void CreateAlgorithmFile(string filePath, string algorithmFileName)
        {
            //Create an Algorithm With the given name and with an empty Algorithm
            Algorithm algorithm = new Algorithm()
            {
                algorithmFileName = algorithmFileName + ".pmalg",
                algorithm = ""
            };

            //Save that algorithm in the given path
            Save(filePath + algorithm.algorithmFileName, algorithm);
        }

        /// <summary>
        /// Creates a Blank StoryConcepts file in a given Path
        /// </summary>
        /// <param name="filePath">The Path to Create the StoryConcepts file in</param>
        /// <param name="storyConceptsfileName">The Name of the StoryConcepts file</param>
        public void CreateStoryConceptsFile(string filePath, string storyConceptsfileName)
        {
            //Create a storyConcepts with the given name an empty data
            StoryConcepts storyConcepts = new StoryConcepts()
            {
                fileName = storyConceptsfileName + ".pmstory",
                StoryTypes = new string[] { "" },
                StoryIdea = "",
                PlotTwists = "",
                PlotPoints = "",
                StoryEvents = ""
            };

            //Save that Story Concepts in the given path
            Save(filePath + storyConcepts.fileName, storyConcepts);
        }

        /// <summary>
        /// Creates a Blank NodeSystem in a given Path
        /// </summary>
        /// <param name="filePath">The Path to Create the NodeSystem file in</param>
        /// <param name="nodeSystemFileName">The Name of the NodeSystem file</param>
        public void CreateNodeSystemFile(string filePath, string nodeSystemFileName)
        {

            //Create a NodeSystem with the given name and Empty Nodes
            NodeSystem nodeSystem = new NodeSystem() 
            {
                fileName = nodeSystemFileName + ".pmnodes",
                Nodes = new List<Node>()
            };

            //Save that NodeSystem in the given Path
            Save(filePath + nodeSystem.fileName, nodeSystem);
        }

        /// <summary>
        /// Creates a new Shedule in a given Path
        /// </summary>
        /// <param name="filePath">The Path to Create the Shedule file in</param>
        /// <param name="SheduleName">The Name of the Shedule file</param>
        public void CreateSheduleFile(string filePath, string SheduleName)
        {
            //Create a Shedule with the given name and with Empty Tasks
            Shedule shedule = new Shedule()
            {
                Name = SheduleName + ".pmshed",
                Tasks = new List<Task>()
            };

            //Save that Shedule in the Given Path
            Save(filePath + shedule.Name, shedule);
        }

        /// <summary>
        /// Creates a new TeamFile in a given Path
        /// </summary>
        /// <param name="filePath">The Path to Create the Team file in</param>
        /// <param name="teamName">The Name of the Team file</param>
        public void CreateTeamFile(string filePath, string teamName)
        {
            //Create a Team with the given name and with Empty Members
            Team team = new Team()
            {
                TeamName = teamName + ".team",
                TeamMembers = new List<TeamMember>()
            };
            
            //Save that Team in the Given Path
            Save(filePath + team.TeamName, team);
        }
        
        /// <summary>
        /// Creates an empty stages file in a given path
        /// </summary>
        /// <param name="filepath">the file path</param>
        /// <param name="StagesFileName">The name of the file</param>
        public void CreateStagesFile(string filepath, string StagesFileName)
        {
            //Create an empty list of stages
            List<Stage> Stages = new List<Stage>();

            //Save that list in the given path
            Save(filepath + StagesFileName, Stages);
        }

        #endregion

    }
}
