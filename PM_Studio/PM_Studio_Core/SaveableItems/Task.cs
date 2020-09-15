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
        private string _TaskDuration;

        #endregion

        #region Constructor
        public Task(string _TaskTitle, string _TaskDescription, string _TaskDuration)
        {
            TaskTitle = _TaskTitle;
            TaskDescription = _TaskDescription;
            TaskDuration = _TaskDuration;
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

        public string TaskDuration
        {
            get
            {
                return _TaskDuration;
            }
            set
            {
                _TaskDuration = value;
            }
        }

        public string TaskTitle {get; set;}
        public string TaskDescription { get; set; }

        
        #endregion

    }
}
