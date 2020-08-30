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
        #endregion

        #region Constructor
        public TeamMemberBlock(TeamMember _teamMember)
        {
            teamMember = _teamMember;
            //Add the row definitions to the Container grid
            Container.RowDefinitions.Add(new RowDefinition() 
            {
                Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto)
            });
            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto)
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

            //Add the 2 TextBlocks to the Container Grid
            Container.Children.Add(lbMemberName);
            Container.Children.Add(lbMemberJob);

            //Set the positions of the two textBlocks
            Grid.SetRow(lbMemberName, 0);
            Grid.SetRow(lbMemberJob, 1);

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

    }
}
