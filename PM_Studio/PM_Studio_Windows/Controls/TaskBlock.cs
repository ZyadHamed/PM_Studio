using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    public class TaskBlock : Border
    {

        #region Variables

        private Task _Task;

        #endregion

        #region Constructor

        public TaskBlock(Task task)
        {
            Task = task;
        }

        #endregion

        Task Task
        {
            get
            {
                return _Task;
            }
            set
            {
                _Task = value;
            }
        }

    }
}
