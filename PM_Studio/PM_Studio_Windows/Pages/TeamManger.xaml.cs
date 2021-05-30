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

            CheckTeam();

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

        void CheckTeam()
        {
            //Make a list with all tean files in the E: directory,(Will be changed soon to the path of the project)
            List<string> TeamFilePaths = FileManger.GetAllFilesByExtension(@"E:\", ".team");
            //If there wasn't any shedule files. show the message and the creation button for the user
            if (TeamFilePaths.Count < 1)
            {
                //Show the  creation of shedule page and hide the tasks grid
                NoTeamGrid.Visibility = Visibility.Visible;
                MainGrid.Visibility = Visibility.Collapsed;
            }

            //If not, use the first file found as a default file opened.
            else
            {
                //Get the first shedule file from the list
                teamMangerViewModel = new TeamMangerViewModel(TeamFilePaths[0]);

                //Show the tasks page and hide the creation of shedule grid
                MainGrid.Visibility = Visibility.Visible;
                NoTeamGrid.Visibility = Visibility.Collapsed;

                //Fill the treeviews with tasks
                ReloadTeamMembers();
            }
        }

        #endregion

        #region Events
        private void lstTeamMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamMangerViewModel.SelectedMember = (e.AddedItems[0] as TeamMemberBlock).TeamMember;
            //Get the Tasks from the selected Team Member and Display it in the TeamMemberTasks ListView
            lstTeamMembersTasks.ItemsSource = teamMangerViewModel.MemberTasks;
        }

        private void btnCreateTeamFile_Click(object sender, RoutedEventArgs e)
        {
            Create_ModifyItemsWindow window = new Create_ModifyItemsWindow(1);
            window.lbDataField1Text = "Tean Name: ";
            if (window.ShowDialog() == true)
            {
                saveLoadSystemViewModel.CreateTeamFile(@"E:\", window.txtDataField1Text);
            }
            CheckTeam();
        }

        #endregion


    }
}
