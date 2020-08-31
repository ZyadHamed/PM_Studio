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

        /// <summary>
        /// Gets All Files of type "Team" in a given filePath
        /// </summary>
        /// <param name="filePath">The File path to search for files in</param>
        /// <returns></returns>
        public List<string> GetAllTeamFiles(string filePath)
        {
            return FileManger.GetAllFilesByExtension(filePath, ".team");
        }


        #endregion

        #region Properties
        /// <summary>
        /// The TeamMembers inside the Team
        /// </summary>
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

        /// <summary>
        /// The Tasks of an Indvdiual Member in the Team
        /// </summary>
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

        /// <summary>
        /// The Current used Team In the Class
        /// </summary>
        public Team Team
        {
            get
            {
                //return the Current Team Variable
                return team;
            }
            set
            {
                //set the Team variable to the incoming value
                team = value;
                //Get All teamMembers inside that new team
                TeamMembers = GetTeamMembers();
            }
        }

        #endregion

    }
}
