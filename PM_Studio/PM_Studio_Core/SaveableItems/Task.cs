using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    public class Task
    {

        #region Variables

        private string _TaskProgress = "Undone";
        private List<TeamMember> _TaskWorkers = new List<TeamMember>();

        #endregion

        #region Constructor
        public Task(string _TaskTitle, string _TaskDescription, string TaskDuration)
        {
            TaskTitle = _TaskTitle;
            TaskDescription = _TaskDescription;
        }
        #endregion

        #region Methods

        public void AddTaskWorker(TeamMember TaskWorker)
        {
            TaskWorkers.Add(TaskWorker);
        }

        #endregion

        #region Properties

        public string TaskProgress
        {
            get
            {
                return _TaskProgress;
            }

            set
            {
                _TaskProgress = value;
            }
        }

        public List<TeamMember> TaskWorkers
        {
            get
            {
                return _TaskWorkers;
            }
            set
            {
                _TaskWorkers = value;
            }
        }

        public string TaskTitle {get; set;}
        public string TaskDescription { get; set; }

        #endregion

    }
}
