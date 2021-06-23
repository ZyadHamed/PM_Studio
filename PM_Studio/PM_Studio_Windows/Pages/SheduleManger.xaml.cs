using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for SheduleManger.xaml
    /// </summary>
    public partial class SheduleManger : Page
    {
        SheduleMangerViewModel sheduleMangerViewModel;
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);
        //Button btnAddTask = new Button();
            
        #region Constructor

        public SheduleManger()
        {
            InitializeComponent();
            
            CheckShedule();

        }

        #endregion

        #region Methods

        void FillTreeView()
        {
            //Set the ItemsSource of all the TreeNodes to null(to remove any Parents to the Items)
            lstCloseTasks.ItemsSource = null;
            lstTasks.ItemsSource = null;

            //Reset the Items Source for each one of them
            lstCloseTasks.ItemsSource = sheduleMangerViewModel.SortedByDeadLineTasks;
            lstTasks.ItemsSource = sheduleMangerViewModel.SortedAlphabiticallyTasks;


        }

        void CheckShedule()
        {
            //Make a list with all shedule files in the E: directory,(Will be changed soon to the path of the project)
            List<string> SheduleFilePaths = FileManger.GetAllFilesByExtension(@"E:\", ".pmshed");
            //If there wasn't any shedule files. show the message and the creation button for the user
            if (SheduleFilePaths.Count < 1)
            {
                //Show the  creation of shedule page and hide the tasks grid
                NoSheduleGrid.Visibility = Visibility.Visible;
                contentGrid.Visibility = Visibility.Collapsed;
            }

            //If not, use the first file found as a default file opened.
            else
            {
                //Get the first shedule file from the list
                sheduleMangerViewModel = new SheduleMangerViewModel(SheduleFilePaths[0]);

                //Show the tasks page and hide the creation of shedule grid
                contentGrid.Visibility = Visibility.Visible;
                NoSheduleGrid.Visibility = Visibility.Collapsed;
                //Fill the treeviews with tasks
                FillTreeView();
            }
        }

        #endregion

        #region Events

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            ////If the Selected Item Was Not a Task Block, Don't do anything
            //if ((tvTasks.SelectedItem as TaskBlock) != null)
            //{
            //    //If it was a Task Block, Store it in a Variable
            //    TaskBlock SelectedItem = tvTasks.SelectedItem as TaskBlock;

            //    //Get the Operation that the User Selected using the MenuItem's name
            //    //If the User Selected Edit Task, Edit the Selected Task
            //    if ((e.Source as MenuItem).Name == "menuItemEditTask")
            //    {
            //        //Create a CreatingItem Window with 2 Data Fields(The Task Title and The Task Duration)
            //        Create_ModifyItemsWindow CreateWindow = new Create_ModifyItemsWindow(2, true);

            //        //Set the First Label Text to Task Title
            //        CreateWindow.lbDataField1Text = "Task Title: ";

            //        //Set the Second Label Text to Task Duration
            //        CreateWindow.lbDataField3Text = "Start Date: \nEndDate: ";

            //        //Set the Title of the Window to Edit Task
            //        CreateWindow.Title = "Edit Task";

            //        //Set the Text Inside the First TextBox to The Text of the Task Title
            //        CreateWindow.txtDataField1Text = SelectedItem.Task.Title;

            //        //Set the Start Date and the End Date Inside the Window to the Start and End Dates of the Task
            //        CreateWindow.StartDate = SelectedItem.Task.StartDate;
            //        CreateWindow.EndDate = SelectedItem.Task.EndDate;

            //        //Show the Window as Dialog
            //        if (CreateWindow.ShowDialog() == true)
            //        {
            //            //Set the Data of the Task to the New Data in the Window
            //            SelectedItem.Task.Title = CreateWindow.txtDataField1Text;
            //            SelectedItem.Task.StartDate = CreateWindow.StartDate;
            //            SelectedItem.Task.EndDate = CreateWindow.EndDate;

            //            //Save the New Data of the Shedule(To save the changes Made in the Task)
            //            sheduleMangerViewModel.SaveSheduleData();

            //            //Refill the TreeView to see the Changes
            //            FillTreeView();
            //        }

            //    }
            //    //If the User Selected Delete Task, Delete the Selected Task
            //    else if ((e.Source as MenuItem).Name == "menuItemDeleteTask")
            //    {
            //        //Remove the Task that is inside the TaskBlock from the Shedule
            //        sheduleMangerViewModel.Shedule.Tasks.Remove(SelectedItem.Task);

            //        //Save the New Data in the Shedule
            //        sheduleMangerViewModel.SaveSheduleData();

            //        //Refill the TreeView to see the Changes
            //        FillTreeView();
            //    }
            //    //If the User Selected Mark Task As Done, Mark the Selected Task as Done
            //    else if ((e.Source as MenuItem).Name == "menuItemMarkTask")
            //    {
            //        //Set the Stored Task in the TaskBlock as Done
            //        SelectedItem.Task.IsDone = true;

            //        //Save the New Data of the Shedule(To save the changes Made in the Task)
            //        sheduleMangerViewModel.SaveSheduleData();

            //        //Refill the TreeView to see the Changes
            //        FillTreeView();
            //    }
            //}

        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            //Create a CreatingItem Window with 2 Data Fields(The Task Title and The Task Duration)
            Create_ModifyItemsWindow CreateWindow = new Create_ModifyItemsWindow(2, true);
            //Set the First Label Text to Task Title
            CreateWindow.lbDataField1Text = "Task Title: ";
            //Set the Second Label Text to Task Duration
            CreateWindow.lbDataField3Text = "Start Date: \n\nEndDate: ";
            //Set the Title of the Window to Create New Task
            CreateWindow.Title = "Create New Task";
            //Show the Window as Dialog
            if (CreateWindow.ShowDialog() == true)
            {
                //If the User Has Pressed the OK button, Add The Task

                //Add a Task using the Data inside the Window
                sheduleMangerViewModel.AddTask(new Task(CreateWindow.txtDataField1Text, "", CreateWindow.StartDate, CreateWindow.EndDate)
                {
                    Progress = "Upcoming"
                });
                //Reload the Items of the Tree View
                FillTreeView();

            }
            
        }

        private void btnCreateShedule_Click(object sender, RoutedEventArgs e)
        {
            Create_ModifyItemsWindow window = new Create_ModifyItemsWindow(1);
            window.lbDataField1Text = "Schedule Name: ";
            if(window.ShowDialog() == true)
            {
                saveLoadSystemViewModel.CreateSheduleFile(@"E:\", window.txtDataField1Text);
            }
            CheckShedule();
        }

        #endregion


    }
}
