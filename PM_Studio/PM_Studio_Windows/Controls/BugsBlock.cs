using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
    public class BugsBlock : Border
    {

        #region Designing Variables
        Grid Container = new Grid();
        Grid TextBlocksContainer = new Grid();
        TextBlock lbBugHeader = new TextBlock();
        TextBlock lbBugDescription = new TextBlock();
        Button btnMore = new Button();
        ContextMenu menu = new ContextMenu();
        MenuItem EditBugMenuItem = new MenuItem();
        MenuItem DeleteBugsMenuItem = new MenuItem();

        #endregion

        #region Variables

        private BugToFix bug;

        #endregion

        #region Constructor

        public BugsBlock(BugToFix _bug)
        {
            Bug = _bug;
            btnMore.Click += btnMore_Click;
            btnMore.Visibility = System.Windows.Visibility.Hidden;
            this.MouseEnter += BugBlock_MouseEnter;
            this.MouseLeave += BugBlock_MouseLeave;
            SetControlsProperties();
            SetControlsData();
        }

        #endregion

        #region Designing Methods

        void SetControlsProperties()
        {
            //Set properties of the textblocks
            lbBugHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EBEBEB"));
            lbBugHeader.FontSize = 20;
            lbBugHeader.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            lbBugDescription.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EBEBEB"));
            lbBugDescription.FontSize = 15;
            lbBugDescription.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

            //Set the properties of the button
            btnMore.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#332F2E"));
            btnMore.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
            btnMore.Content = "...";
            btnMore.FontSize = 15;
            btnMore.Padding = new System.Windows.Thickness(10, 0, 10, 15);
            btnMore.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            //Set the properties of the context menu
            EditBugMenuItem.Header = "Edit Bug";
            DeleteBugsMenuItem.Header = "Delete Bug";

            menu.Items.Add(EditBugMenuItem);
            menu.Items.Add(DeleteBugsMenuItem);

            //Add rows to the textblocks container grid
            TextBlocksContainer.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            TextBlocksContainer.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });

            //Add the textblocks to their container grid and set their position
            TextBlocksContainer.Children.Add(lbBugHeader);
            TextBlocksContainer.Children.Add(lbBugDescription);

            Grid.SetRow(lbBugHeader, 0);
            Grid.SetRow(lbBugDescription, 1);

            //Add columns to the container grid
            Container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            Container.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto) });

            //Add the Controls to their container grid and set their position
            Container.Children.Add(TextBlocksContainer);
            Container.Children.Add(btnMore);

            Grid.SetColumn(TextBlocksContainer, 0);
            Grid.SetColumn(btnMore, 1);


            btnMore.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            this.Child = Container;
            this.CornerRadius = new System.Windows.CornerRadius(4);
            this.BorderThickness = new System.Windows.Thickness(2);
            this.BorderBrush = Brushes.LightGray;
            this.Padding = new System.Windows.Thickness(5);
            this.Margin = new System.Windows.Thickness(5,10,5,10);

        }

        #endregion

        #region Methods

        void SetControlsData()
        {
            lbBugHeader.Text = Bug.Header;
            lbBugDescription.Text = Bug.Description;
        }

        #endregion

        #region Events

        private void btnMore_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }

        private void BugBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMore.Visibility = System.Windows.Visibility.Visible;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D3837"));
        }

        private void BugBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMore.Visibility = System.Windows.Visibility.Hidden;
            this.Background = Brushes.Transparent;
        }

        #endregion

        #region Properties

        public BugToFix Bug
        {
            get
            {
                return bug;
            }

            set
            {
                bug = value;
                SetControlsData();
            }
        }

        #endregion

    }
}
