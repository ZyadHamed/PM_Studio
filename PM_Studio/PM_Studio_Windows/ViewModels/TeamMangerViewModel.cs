using System;
using System.Collections.Generic;
using System.Linq;

namespace PM_Studio
{
   public class TeamMangerViewModel
    {
        #region Variables

        Team team;
        string teamFilePath = "";
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);

        //Properties private Variables
        private List<TeamMemberBlock> _TeamMembers;
        private TeamMember _SelectedMember;
        private List<string> _MemberTasks;

        #endregion

        #region Constructor
        public TeamMangerViewModel(Team _team, string _teamFilePath)
        {
            team = _team;
            teamFilePath = _teamFilePath;
           
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

        /// <summary>
        /// Saves the current data of the team inside the teamFilepath
        /// </summary>
        void SaveTeamData()
        {
            saveLoadSystemViewModel.Save(teamFilePath, team);
        }

        /// <summary>
        /// Removes a TeamMember From the Team
        /// </summary>
        /// <param name="teamMember">The TeamMember to remove</param>
        public void RemoveTeamMember(TeamMember teamMember)
        {
            int i = 0;
            //Loop inside all TeamMembers
            foreach(TeamMember member in team.TeamMembers)
            {
                //If the data of the current teamMember in the Loop Matches the data of the given TeamMember
                if(member.Name == teamMember.Name && member.Job == teamMember.Job && Enumerable.SequenceEqual(member.Tasks, teamMember.Tasks))
                {
                    //Remove that teamMember using the index of him
                    team.TeamMembers.RemoveAt(i);
                    //Break the Operation
                    break;
                }
                i++;
            }
            //Set the TeamMember property to the TeamMembers of the team
            TeamMembers = GetTeamMembers();
            //Save the current data of the team
            SaveTeamData();
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
                //Set the private variable to the TeamMembers of the team
                _TeamMembers =  GetTeamMembers();
                //Return the private variable
                return _TeamMembers;
            }
            set
            {
                //Set the private variable to the incoming value
                _TeamMembers = value;
            }
        }

        /// <summary>
        /// The Tasks of the Selected Member in the Team
        /// </summary>
        public List<string> MemberTasks
        {
            get
            {
               //Set The Value of the private MemberTasks Variable to the SelectedMember Tasks
                _MemberTasks = SelectedMember.Tasks;
                //return the value of that private variable 
                return _MemberTasks;
            }
            set
            {
                //Set the private variable to the incoming Tasks
                _MemberTasks = value;
                //Set the Tasks of the Selected Member to the incoming Tasks
                SelectedMember.Tasks = value;
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

        /// <summary>
        /// The Current Member in Memory
        /// </summary>
        public TeamMember SelectedMember
        {
            get
            {
                //Return the private variable holding the Selected Member
                return _SelectedMember;
            }
            set
            {
                //Set the private selected member to the incoming value
                _SelectedMember = value;
                //Set the MemberTasks to the tasks of the current member
                MemberTasks = value.Tasks;
            }
        }

        #endregion

    }
}
