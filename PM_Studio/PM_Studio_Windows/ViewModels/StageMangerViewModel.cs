using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    public class StageMangerViewModel
    {

        #region Variables
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);

        private List<Stage> stages;

        #endregion

        #region Constructor

        public StageMangerViewModel(string stageMangerFilePath)
        {
            StageMangerFilePath = stageMangerFilePath;
            Stages = saveLoadSystemViewModel.GetStages(stageMangerFilePath);
        }

        #endregion

        #region Methods

        void GetStages()
        {
            //Create three empty lists for each type of stages
            List<StageBlock> upcomingStages = new List<StageBlock>();
            List<StageBlock> inProgressStages = new List<StageBlock>();
            List<StageBlock> doneStages = new List<StageBlock>();

            //Now loop on all stages in the Stages list
            foreach(Stage stage in Stages)
            {
                //Create a block based on that stage
                StageBlock block = new StageBlock(stage);

                //Classify this block according to the type of stage it's based on
                //Add each block to it's corrosponding list
                if(stage.StageType == "Upcoming")
                {
                    upcomingStages.Add(block);
                }

                else if (stage.StageType == "In Progress")
                {
                    inProgressStages.Add(block);
                }

                else if (stage.StageType == "Done")
                {
                    doneStages.Add(block);
                }
            }

            //Set all stageblock types lists to their values;
            UpcomingStages = upcomingStages;
            InProgressStages = inProgressStages;
            DoneStages = doneStages;
        }

        public void AddStage(Stage stage)
        {
            Stages.Add(stage);
            saveLoadSystemViewModel.Save(StageMangerFilePath, Stages);
            Stages = saveLoadSystemViewModel.GetStages(StageMangerFilePath);
            //GetStages();
        }

        public void RemoveStage(Stage stage)
        {
            Stages.Remove(stage);
            GetStages();
        }

        #endregion

        #region Properties

        public List<Stage> Stages
        {
            get
            {
                return stages;
            }
            set
            {
                stages = value;
                GetStages();
            }
        }

        public List<StageBlock> UpcomingStages { get; set; }

        public List<StageBlock> InProgressStages { get; set; }

        public List<StageBlock> DoneStages { get; set; }

        public string StageMangerFilePath { get; set; }

        #endregion

    }
}
