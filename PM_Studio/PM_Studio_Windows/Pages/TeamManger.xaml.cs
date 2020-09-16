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
        public TeamMangerViewModel teamMangerViewModel;
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);
        #endregion

        #region Constructor
        public TeamManger()
        {
            InitializeComponent();
           
            //Intilaize the TeamMangerViewModel Class with the team paramerter
            teamMangerViewModel = new TeamMangerViewModel(saveLoadSystemViewModel.GetTeam(@"E:\zyadhamedashour\team1.team"), @"E:\zyadhamedashour\team1.team");
            //Set the Item Source of the TeamMembers ListView to the TeamMembers from the View Model
            lstTeamMembers.ItemsSource = teamMangerViewModel.TeamMembers;

            // the Code that was used for testing the Functionallity of teamfiles:

            ////1-Create a List of TeamMembers
            // List<TeamMember> teamMembers = new List<TeamMember>();

            ////2-Add Some TeamMembers to that List
            //teamMembers.Add(new TeamMember()
            //{
            //    Name = "Ahmed",
            //    Job = "Senior Programmer",
            //    Tasks = new List<string>() { "Revise the code", "Help his team" }
            //});
            //teamMembers.Add(new TeamMember()
            //{
            //    Name = "Ali",
            //    Job = "Junior Programmer",
            //    Tasks = new List<string>() { "Write Some Basic Code", "Do some basic tasks" }
            //});
            //teamMembers.Add(new TeamMember()
            //{
            //    Name = "Kamel",
            //    Job = "Game Designer",
            //    Tasks = new List<string>() { "Plan how the game will go", "Design the enviroment of the game", "block out the base of the game" }
            //});
            //teamMembers.Add(new TeamMember()
            //{
            //    Name = "Basel",
            //    Job = "Senior Designer",
            //    Tasks = new List<string>() { "Revise the work of the Game Designers", "Design the details of enviroment of the game", "Design the characters" }
            //});

            ////Make a Team From that List of TeamMembers and give that Team a Name
            //Team team = new Team()
            //{
            //    TeamName = "The Devs",
            //    TeamMembers = teamMembers
            //};

            ////Save that Team In a Path
            //saveLoadSystemViewModel.Save(@"E:\zyadhamedashour\team1.team", team);
        }

        #endregion

        #region Methods

        public void ReloadTeamMembers()
        {
            //Empty the TeamMembers ListView
            lstTeamMembers.ItemsSource = null;
            //Set the Item Source of the TeamMembers ListView to the TeamMembers from the View Model
            lstTeamMembers.ItemsSource = teamMangerViewModel.TeamMembers;
        }

        #endregion

        #region Events
        private void lstTeamMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamMangerViewModel.SelectedMember = (e.AddedItems[0] as TeamMemberBlock).TeamMember;
            //Get the Tasks from the selected Team Member and Display it in the TeamMemberTasks ListView
            lstTeamMembersTasks.ItemsSource = teamMangerViewModel.MemberTasks;
        }

        #endregion

    }
}
