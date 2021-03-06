﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        private List<TaskBlock> GetTaskBlocks()
        {
            //Create an Empty List of TaskBlocks
            List<TaskBlock> taskBlocks = new List<TaskBlock>();
            
            //Loop inside each task in the current shedule
            foreach (Task task in Shedule.Tasks)
            {
                //Create a TaskBlock Based on that task
                TaskBlock taskBlock = new TaskBlock(task);
                //Add that TaskBlock to the List
                taskBlocks.Add(taskBlock);

            }
            //Save the Current Data of the Tasks(Will take affect when the Progress of a Task Changes)
            SaveSheduleData();
            //Return the List of the TaskBlocks
            return taskBlocks;
            
        }

        private (List<TaskBlock> UpcomingTasks, List<TaskBlock> InProgrssTasks, List<TaskBlock> DoneTasks, List<TaskBlock> UndoneTasks) SortTasksByType()
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
                if (taskBlock.Task.Tag == "Upcoming")
                {
                    UpcomingTasks.Add(taskBlock);
                }
                //If the Task of that task Block was In Progress, add it to the InProgress TaskBlocks List
                else if (taskBlock.Task.Tag == "In Progress")
                {
                    InProgrssTasks.Add(taskBlock);
                }
                //If the Task of that task Block was Done, add it to the Done TaskBlocks List
                else if (taskBlock.Task.Tag == "Done")
                {
                    DoneTasks.Add(taskBlock);
                }
                //If the Task of that task Block was Undone, add it to the Undone TaskBlocks List
                else if (taskBlock.Task.Tag == "Undone")
                {
                    UndoneTasks.Add(taskBlock);
                }
                
            }
            return (UpcomingTasks, InProgrssTasks, DoneTasks, UndoneTasks);
        }

        /// <summary>
        /// Sorts the tasks by their deadline
        /// </summary>
        /// <returns>a list that containes all the tasks sorted by the nearst deadline</returns>
        private List<TaskBlock> SortTasksByDeadline()
        {
            return GetTaskBlocks().OrderBy(x => x.Task.EndDate).ToList();
        }

        /// <summary>
        /// Sortes the task in acesnding order according to the title of the task
        /// </summary>
        /// <returns>a list that contains all the tasks sorted by the title in ascending order</returns>
        private List<TaskBlock> SortTasksInAlphabiticalOrder()
        {
            return GetTaskBlocks().OrderBy(x => x.Task.Title).ToList();
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
                return SortTasksByType().UpcomingTasks;
            }
        }

        /// <summary>
        /// The In Progress Tasks Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> InProgressTasks
        {
            get
            {
                return SortTasksByType().InProgrssTasks;
            }
        }

        /// <summary>
        /// The Done Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> DoneTasks
        {
            get
            {
                return SortTasksByType().DoneTasks;
            }
        }

        /// <summary>
        /// The Undone Tasks returned from the Tasks Sorting Method
        /// </summary>
        public List<TaskBlock> UndoneTasks
        {
            get
            {
                return SortTasksByType().UndoneTasks;
            }
        }

        public List<TaskBlock> SortedByDeadLineTasks
        {
            get
            {
                return SortTasksByDeadline();
            }
        }

        public List<TaskBlock> SortedAlphabiticallyTasks
        {
            get
            {
                return SortTasksInAlphabiticalOrder();
            }
        }

        #endregion

    }
}
