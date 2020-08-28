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
        TeamMangerViewModel teamMangerViewModel;
        public TeamManger()
        {
            InitializeComponent();
            List<TeamMember> teamMembers = new List<TeamMember>();
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
            Team team = new Team()
            {
                TeamName = "The ROcks",
                TeamMembers = teamMembers
            };
            teamMangerViewModel = new TeamMangerViewModel(team);
            lstTeamMembers.ItemsSource = teamMangerViewModel.TeamMembers;
            
        }

        private void lstTeamMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstTeamMembersTasks.ItemsSource = ((TeamMember)(e.AddedItems[0] as TeamMemberBlock).Tag).Tasks
        }
    }
}
