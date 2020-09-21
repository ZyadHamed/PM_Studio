using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    public class Task
    {

        #region Variables

        private string _Progress = "Upcoming";
        private List<TeamMember> _TaskWorkers = new List<TeamMember>();
        private DateTime _StartDate;
        private DateTime _EndDate;
        private bool _IsDone;

        #endregion

        #region Constructor
        public Task(string _TaskTitle, string _TaskDescription, DateTime _StartDate, DateTime _EndDate, bool _IsDone = false)
        {
            TaskTitle = _TaskTitle;
            TaskDescription = _TaskDescription;
            StartDate = _StartDate;
            EndDate = _EndDate;
            IsDone = _IsDone;

            SetTaskProgress();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Adds a TeamMember that is working on that specific Task
        /// </summary>
        /// <param name="TaskWorker">The TeamMember that will work on the Task</param>
        public void AddTaskWorker(TeamMember TaskWorker)
        {
            TaskWorkers.Add(TaskWorker);
        }

        void SetTaskProgress()
        {
            //If the task was Done, set the Progress to Done
            if (IsDone == true)
            {
                Progress = "Done";
            }
            //If not, check the progress according to the Dates of the Task
            else
            {
                //If The Start Date of the Task was Later than Today, the Task is Upcoming
                if (DateTime.Compare(DateTime.Now, StartDate) < 0)
                {
                    Progress = "Upcoming";
                }
                else
                {
                    //If the start Date has Already arrived but the end date is not, the Task is in Progress
                    if (DateTime.Compare(DateTime.Now, StartDate) >= 0 && DateTime.Compare(DateTime.Now, EndDate) < 0)
                    {
                        Progress = "In Progress";
                    }
                    //If the Start Date has Already arrived and the End Date also arrived and the Task is not done yet, then the task is Undone
                    else if (DateTime.Compare(DateTime.Now, StartDate) >= 0 && DateTime.Compare(DateTime.Now, EndDate) > 0)
                    {
                        Progress = "Undone";
                    }
                }
            }
            
        }

        #endregion

        #region Properties

        public string Progress
        {
            get
            {
                return _Progress;
            }

            set
            {
                _Progress = value;
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

        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
                SetTaskProgress();
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
                SetTaskProgress();
            }
        }

        public bool IsDone
        {
            get
            {
                return _IsDone;
            }
            set
            {
              _IsDone = value;
            }
        }

        public string TaskTitle {get; set;}
        public string TaskDescription { get; set; }

        
        #endregion

    }
}
