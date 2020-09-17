using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    class SheduleMangerViewModel
    {

        #region Methods

        public (List<TaskBlock> UndoneTasks, List<TaskBlock> InProgrssTasks, List<TaskBlock> DoneTasks) SortTasks(List<TaskBlock> TaskBlocks)
        {
            List<TaskBlock> UndoneTasks = new List<TaskBlock>();
            List<TaskBlock> InProgrssTasks = new List<TaskBlock>();
            List<TaskBlock> DoneTasks = new List<TaskBlock>();
            foreach(TaskBlock taskBlock in TaskBlocks)
            {
                if(taskBlock.Task.TaskProgress == "Undone")
                {
                    UndoneTasks.Add(taskBlock);
                }
                else if (taskBlock.Task.TaskProgress == "In Progress")
                {
                    InProgrssTasks.Add(taskBlock);
                }
                else if (taskBlock.Task.TaskProgress == "Done")
                {
                    DoneTasks.Add(taskBlock);
                }
            }
            return (UndoneTasks, InProgrssTasks, DoneTasks);
        }

        #endregion

    }
}
