using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for TeamManger.xaml
    /// </summary>
    public partial class TeamManger : Page
    {
        #region Variables
        TeamMangerViewModel teamMangerViewModel;
        #endregion

        #region Constructor
        public TeamManger()
        {
            InitializeComponent();
            //Define a List of teamMembers
            List<TeamMember> teamMembers = new List<TeamMember>();
            //Add Some TeamMembers to that List for testing
            teamMembers.Add(new TeamMember()
            {
                Name = "Ahmed",
                Job = "Senior Programmer",
                Tasks = new List<string>(){ "Revise the code", "Help his team" }
            });
            teamMembers.Add(new TeamMember()
            {
                Name = "Ali",
                Job = "Junior Programmer",
                Tasks = new List<string>() { "Write Some Basic Code", "Do some basic tasks" }
            });
            teamMembers.Add(new TeamMember()
            {
                Name = "Kamel",
                Job = "Game Designer",
                Tasks = new List<string>() { "Plan how the game will go", "Design the enviroment of the game", "block out the base of the game" }
            });
            //Make a Team From that List of TeamMembers and give that Team a Name
            Team team = new Team()
            {
                TeamName = "The ROcks",
                TeamMembers = teamMembers
            };
            //Intilaize the TeamMangerViewModel Class with the team paramerter
            teamMangerViewModel = new TeamMangerViewModel(team);
            //Set the Item Source of the TeamMembers ListView to the TeamMembers from the View Model
            lstTeamMembers.ItemsSource = teamMangerViewModel.TeamMembers;
            
        }

        #endregion

        #region Events
        private void lstTeamMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the Tasks from the selected Team Member and Display it in the TeamMemberTasks ListView
            lstTeamMembersTasks.ItemsSource = ((TeamMember)(e.AddedItems[0] as TeamMemberBlock).Tag).Tasks;
        }

        #endregion

    }
}
