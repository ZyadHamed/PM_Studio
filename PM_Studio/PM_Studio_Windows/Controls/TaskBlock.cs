using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
    public class TaskBlock : Border
    {

        #region Designing Variables

        StackPanel Container = new StackPanel();
        TextBlock lbTaskTitle = new TextBlock();
        StackPanel TaskDateContainer = new StackPanel();
        Image TaskDateIcon = new Image();
        TextBlock lbTaskDate = new TextBlock();
        DockPanel BottomContainer = new DockPanel();
        Image TaskProgressIcon = new Image();
        TextBlock lbTaskProgess = new TextBlock();
        Image TaskWorkersIcon = new Image();

        #endregion

        #region Variables

        private Task _Task;

        #endregion

        #region Constructor

        public TaskBlock(Task task)
        {
            Task = task;

            SetControlsProperties();
            AddControlsToContainer();
            SetBlockData();
            
        }

        #endregion

        #region Methods

        void SetControlsProperties()
        {
            //Set the Properties of the outer Border
            this.BorderBrush = Brushes.Transparent;
            this.Background = new SolidColorBrush(Color.FromRgb(66, 64, 71));
            this.BorderThickness = new System.Windows.Thickness(3);
            this.CornerRadius = new System.Windows.CornerRadius(20);
            this.Margin = new System.Windows.Thickness(5);
            this.Padding = new System.Windows.Thickness(5);

            //Set the Margin of the Container StackPanel
            Container.Margin = new System.Windows.Thickness(5);

            //Set the Properties of the Task Title TextBlock
            lbTaskTitle.FontSize = 25;
            lbTaskTitle.Foreground = Brushes.FloralWhite;

            //Set the Properties of the TaskDateContainer StackPanel
            TaskDateContainer.Orientation = Orientation.Horizontal;
            TaskDateContainer.Margin = new System.Windows.Thickness(0,10,0,0);

            //Set the Width of the TaskDateIcon(Source of the Icon will be added soon)
            TaskDateIcon.Width = 20;

            //Set the Foreground and the FontSize of the TaskDate Label
            lbTaskDate.Foreground = Brushes.FloralWhite;
            lbTaskDate.FontSize = 14;

            //Add Some margin to the bottom Container
            BottomContainer.Margin = new System.Windows.Thickness(0, 10, 0, 0);

            //Set the Width of the TaskProgressIcon(Source of the Icon will be added soon)
            TaskProgressIcon.Width = 20;

            //Set the font size of the TaskProgress Label
            lbTaskProgess.FontSize = 14;

            //Set the Width and the HorizontalAlignment of the TaskWorkersIcon(Source of the Icon will be added soon)
            TaskWorkersIcon.Width = 20;
            TaskWorkersIcon.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        }

        /// <summary>
        /// Adds all the Controls inside the class to their correct Container
        /// </summary>
        void AddControlsToContainer()
        {
            //Add the TaskProgressIcon, TaskProgrssLabel, and the TaskWorkersIcon to the Bottom Container
            BottomContainer.Children.Add(TaskProgressIcon);
            BottomContainer.Children.Add(lbTaskProgess);
            BottomContainer.Children.Add(TaskWorkersIcon);

            //Add the TaskDateIcon and the TaskDateLabel to the TaskDate Container
            TaskDateContainer.Children.Add(TaskDateIcon);
            TaskDateContainer.Children.Add(lbTaskDate);

            //Add the TaskTitle,the TaskDateContainer, and the BottomContainer to the Container StackPanel
            Container.Children.Add(lbTaskTitle);
            Container.Children.Add(TaskDateContainer);
            Container.Children.Add(BottomContainer);

            //Add the Container StackPanel to the Border
            this.Child = Container;
        }

        /// <summary>
        /// Sets the Data inside the Block with respect to the Task passed to the class
        /// </summary>
        void SetBlockData()
        {
            //Set the Task Title and Task Duration labels to the Title and Duration of the Task
            lbTaskTitle.Text = Task.Title;
            lbTaskDate.Text = Task.StartDateTimeStamp.GetDateTime().ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " To " + Task.EndDateTimeStamp.GetDateTime().ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            //Reset the Progress of the Task to avoid Errors in the Progress of the Task
            Task.SetTaskProgress();
            //If the Task Progress was Upcoming,  display "Upcoming" in the Progress label with Purple Color
            if (Task.Progress == "Upcoming")
            {
                lbTaskProgess.Text = "Upcoming";
                lbTaskProgess.Foreground = Brushes.Purple;
            }

            //else if the Task Progress was In Progress, display "In Progress" in the Progress label with yellow Color
            else if (Task.Progress == "In Progress")
            {
                lbTaskProgess.Text = "In Progress";
                lbTaskProgess.Foreground = Brushes.Yellow;
            }

            //else if the Task Progress was Done, display "Done" in the Progress label with Lime Color
            else if (Task.Progress == "Done")
            {
                lbTaskProgess.Text = "Done";
                lbTaskProgess.Foreground = Brushes.Lime;
            }


            //else if the Task Progress was Undone, display "Undone" in the Progress label with red Color
            else if (Task.Progress == "Undone")
            {
                lbTaskProgess.Text = "Undone";
                lbTaskProgess.Foreground = Brushes.Red;
            }

           
        }

        #endregion
   
        #region Properties

        /// <summary>
        /// The Task in which this TaskBlock is based On
        /// </summary>
        public Task Task
        {
            get
            {
                return _Task;
            }
            set
            {
                //Set the Block Task to the incoming value
                _Task = value;
                //Set the Data of the Block to the new data of the incoming task
                SetBlockData();
            }
        }

        #endregion

    }
}
