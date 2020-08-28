using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Text;
using System.Windows.Navigation;

namespace PM_Studio
{
    class TeamMangerViewModel
    {
        #region Variables
        Team team;
        #endregion

        #region Constructor
        public TeamMangerViewModel(Team _team)
        {
            team = _team;
        }
        #endregion

        #region Methods
        public List<TeamMemberBlock> GetTeamMembers()
        {
            List<TeamMemberBlock> returnedList = new List<TeamMemberBlock>();
            foreach (var Teammember in team.TeamMembers)
            {
                TeamMemberBlock teamMemberBlock = new TeamMemberBlock(Teammember);
                teamMemberBlock.Tag = Teammember;
                returnedList.Add(teamMemberBlock);
                
            }
            return returnedList;
        }

        #endregion

        #region Properties
        public List<TeamMemberBlock> TeamMembers
        {
            get
            {
                return GetTeamMembers();
            }
            set
            {
                TeamMembers = value;
            }
        }

        public List<string> MemberTasks
        {
            get
            {
                return null;
            }
            set
            {
                MemberTasks = value;
            }
        }

        

     
        #endregion
    }
}
