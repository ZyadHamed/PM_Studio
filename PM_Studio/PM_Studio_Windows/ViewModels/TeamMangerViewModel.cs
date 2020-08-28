using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
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
            //Make a List of TeamMemberBlock to be the Container of the Data of the Team Member
            List<TeamMemberBlock> teamMemberBlocks = new List<TeamMemberBlock>();

            //Loop inside each TeamMember in the TeamMembers inside the Team
            foreach (TeamMember Teammember in team.TeamMembers)
            {
                //Define a TeamMemberBlock based on the data of the teamMember
                TeamMemberBlock teamMemberBlock = new TeamMemberBlock(Teammember);
                //Set the Tag to be that teamMember so we can use that teamMember again in other Classes
                teamMemberBlock.Tag = Teammember;
                //Add that TeamMemberBlock to the List of TeamMemberBlocks
                teamMemberBlocks.Add(teamMemberBlock);
                
            }
            return teamMemberBlocks;
        }

        #endregion

        #region Properties
        public List<TeamMemberBlock> TeamMembers
        {
            get
            {
                //return a List of TeamMembers from the GetTeamMembers Method
                return GetTeamMembers();
            }
            set
            {
                //Set the property to the incoming value
                TeamMembers = value;
            }
        }

        public List<string> MemberTasks
        {
            get
            {
                //Return null as an intial return (will be changed later)
                return null;
            }
            set
            {
                //Set the property to the incoming value
                MemberTasks = value;
            }
        }

        #endregion

    }
}
