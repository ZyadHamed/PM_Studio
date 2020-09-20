using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    class SheduleMangerViewModel
    {

        #region Variables
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);

        private Shedule _Shedule;
        private string _SheduleFilePath;

        #endregion

        #region Constructor

        public SheduleMangerViewModel(string sheduleFilePath)
        {
            //Set the Shedule of the Class to the Shedule inside the file in the filePath
            Shedule = saveLoadSystemViewModel.GetShedule(sheduleFilePath);
            //Set the SheduleFilePath to that file path
            SheduleFilePath = sheduleFilePath;
        }

        #endregion

        #region Methods

        public List<TaskBlock> GetTaskBlocks()
        {
            //Create an Empty List of TaskBlocks
            List<TaskBlock> taskBlocks = new List<TaskBlock>();
            
            //Loop inside each task in the current shedule
            foreach(Task task in Shedule.Tasks)
            {
                //Create a TaskBlock Based on that task
                TaskBlock taskBlock = new TaskBlock(task);
                //Add that TaskBlock to the List
                taskBlocks.Add(taskBlock);
            }
            //Return the List of the TaskBlocks
            return taskBlocks;
        }

        public (List<TaskBlock> UpcomingTasks, List<TaskBlock> InProgrssTasks, List<TaskBlock> DoneTasks, List<TaskBlock> UndoneTasks) SortTasks()
        {
            //Create 4 Empty Lists for each type of TaskBlocks
            List<TaskBlock> UpcomingTasks = new List<TaskBlock>();
            List<TaskBlock> InProgrssTasks = new List<TaskBlock>();
            List<TaskBlock> DoneTasks = new List<TaskBlock>();
            List<TaskBlock> UndoneTasks = new List<TaskBlock>();
            
            //Loop inside all TaskBlocks in the Shedule
            foreach (TaskBlock taskBlock in GetTaskBlocks())
            {
                //If the Task of that task Block was Upcoming, add it to the Upcoming TaskBlocks List
                if (taskBlock.Task.TaskProgress == "Upcoming")
                {
                    UpcomingTasks.Add(taskBlock);
                }
                //If the Task of that task Block was In Progress, add it to the InProgress TaskBlocks List
                else if (taskBlock.Task.TaskProgress == "In Progress")
                {
                    InProgrssTasks.Add(taskBlock);
                }
                //If the Task of that task Block was Done, add it to the Done TaskBlocks List
                else if (taskBlock.Task.TaskProgress == "Done")
                {
                    DoneTasks.Add(taskBlock);
                }
                //If the Task of that task Block was Undone, add it to the Undone TaskBlocks List
                else if (taskBlock.Task.TaskProgress == "Undone")
                {
                    UndoneTasks.Add(taskBlock);
                }
                
            }
            return (UpcomingTasks, InProgrssTasks, DoneTasks, UndoneTasks);
        }

        /// <summary>
        /// Saves the Data of the shedule in the shedule filePath
        /// </summary>
        public void SaveSheduleData()
        {
            saveLoadSystemViewModel.Save(SheduleFilePath, Shedule);
        }

        /// <summary>
        /// Adds a Task to the Shedule and Saves the Changes
        /// </summary>
        /// <param name="task">The Task to Add</param>
        public void AddTask(Task task)
        {
            Shedule.Tasks.Add(task);
            SaveSheduleData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Current Shedule Used in the Class
        /// </summary>
        public Shedule Shedule
        {
            get
            {
                return _Shedule;
            }
            set
            {
                _Shedule = value;
                
            }
        }

        /// <summary>
        /// The filePath of the Shedule file
        /// </summary>
        public string SheduleFilePath
        {
            get
            {
                return _SheduleFilePath;
            }
            set
            {
                _SheduleFilePath = value;
                Shedule = saveLoadSystemViewModel.GetShedule(_SheduleFilePath);
            }
        }

        /// <summary>
        /// The UpcomingTasks Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> UpcomingTasks
        {
            get
            {
                return SortTasks().UpcomingTasks;
            }
        }

        /// <summary>
        /// The In Progress Tasks Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> InProgressTasks
        {
            get
            {
                return SortTasks().InProgrssTasks;
            }
        }

        /// <summary>
        /// The Done Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> DoneTasks
        {
            get
            {
                return SortTasks().DoneTasks;
            }
        }

        /// <summary>
        /// The Undone Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> UndoneTasks
        {
            get
            {
                return SortTasks().UndoneTasks;
            }
        }

        #endregion

    }
}
