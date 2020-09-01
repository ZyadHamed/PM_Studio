using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
   public class TeamMemberBlock : Border
    {
        #region Variables
        TeamMember teamMember;
        TextBlock lbMemberName = new TextBlock();
        TextBlock lbMemberJob = new TextBlock();
        Grid Container = new Grid();
        StackPanel TextContainer = new StackPanel();
        Button btnMore = new Button();

        ContextMenu MoreMenu = new ContextMenu();
        MenuItem RemoveTeamMemberMenuItem = new MenuItem();
        MenuItem EditTeamMemberMenuItem = new MenuItem();

        #endregion

        #region Constructor
        public TeamMemberBlock(TeamMember _teamMember)
        {
            teamMember = _teamMember;
            //Add the row definitions to the Container grid
            Container.ColumnDefinitions.Add(new ColumnDefinition() 
            {
                Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
            });
            Container.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto)
            });

            //Set the Text of the two TextBlocks to the Name and the Job of the Team Member
            lbMemberName.Text = teamMember.Name;
            lbMemberJob.Text = teamMember.Job;

            //Set the Font Size of the TextBlocks to 17
            lbMemberName.FontSize = 17;
            lbMemberJob.FontSize = 17;

            //Set the Foreground of the TextBlocks
            lbMemberName.Foreground = Brushes.WhiteSmoke;
            lbMemberJob.Foreground = Brushes.WhiteSmoke;

            //Set the Text and font size of the Button
            btnMore.Content = "⋮";
            btnMore.FontSize = 17;

            //Set the Header for the menu Items
            RemoveTeamMemberMenuItem.Header = "Remove Team Member";
            EditTeamMemberMenuItem.Header = "Edit Team Member";

            //Add the Buttons Clicked Event
            btnMore.Click += moreButton_Click;

            //Add the Menu Items Click Events
            RemoveTeamMemberMenuItem.Click += btnRemoveTeamMember_Click;
            EditTeamMemberMenuItem.Click += btnEditTeamMember_Click;

            //Add the Items to the menu and Set the Context Menu of btnMore
            MoreMenu.Items.Add(EditTeamMemberMenuItem);
            MoreMenu.Items.Add(RemoveTeamMemberMenuItem);
            btnMore.ContextMenu = MoreMenu;

            //Add the 2 TextBlocks to the Container StackPanel
            TextContainer.Children.Add(lbMemberName);
            TextContainer.Children.Add(lbMemberJob);

            //Add the Container StackPanel and the More Button to the Grid
            Container.Children.Add(TextContainer);
            Container.Children.Add(btnMore);

            //Set the positions of the TextBlocks Container and the More Button
            Grid.SetColumn(TextContainer, 0);
            Grid.SetColumn(btnMore, 1);

            //Add Some Margin between the Blocks
            this.Margin = new System.Windows.Thickness(5, 10, 5, 10);

            //Add Some Padding Between the text and the Border
            this.Padding = new System.Windows.Thickness(5, 10, 5, 10);

            //Change the Color of the Surrounding Border to Dim Gray
            this.BorderBrush = Brushes.DimGray;

            //Add Some Thickness to the Border
            this.BorderThickness = new System.Windows.Thickness(2);

            //Add Some Rounded corners to the Border
            this.CornerRadius = new System.Windows.CornerRadius(1);

            //Add the Container Grid to be a child of the Border
            this.Child = Container;
        }

        #endregion

        #region Events

        private void moreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoreMenu.IsOpen = true;
        }

        private void btnRemoveTeamMember_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Make an Instance of the TeamManger Page
            TeamManger teamManger = new TeamManger();
            //Remove the TeamMember using the teamMangerViewModel inside the TeamManger Class
            //(Will Update the code soon to add refreashing of the Page)
            teamManger.teamMangerViewModel.RemoveTeamMember(this.Tag as TeamMember);
            
        }

        private void btnEditTeamMember_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }


        #endregion

    }
}
