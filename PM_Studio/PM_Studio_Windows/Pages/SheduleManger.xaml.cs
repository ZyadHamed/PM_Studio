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
        SheduleMangerViewModel sheduleMangerViewModel = new SheduleMangerViewModel(@"E:\zyadhamedashour\PMShedule.pmshed");
        //Button btnAddTask = new Button();
        #region Constructor
        public SheduleManger()
        {
            InitializeComponent();

            ////Code Used for Creating a shedule file
            /////note: Code was Updated so you need to create a new SheduleFile so that the Tasks in it are affected with the Changes
            //Task task1 = new Task("A", "", "From 9/9/2020 To 15/12/2020");
            //task1.TaskProgress = "Done";
            //Task task2 = new Task("B", "", "From 8/8/2020 To 8/9/2020");
            //task2.TaskProgress = "Done";
            //Task task3 = new Task("C", "", "From 9/10/2020 To 5/11/2020");
            //task3.TaskProgress = "In Progress";
            //Task task4 = new Task("D", "", "From 12/12/2020 To 5/1/2021");
            //task4.TaskProgress = "Undone";
            //Task task5 = new Task("E", "", "From 5/1/2021 To 15/1/2021");
            //task5.TaskProgress = "Undone";
            //List<Task> tasks = new List<Task>();
            //tasks.Add(task1);
            //tasks.Add(task2);
            //tasks.Add(task3);
            //tasks.Add(task4);
            //tasks.Add(task5);
            //Shedule shedule = new Shedule()
            //{
            //    Name = "PMShedule.pmshed",
            //    Tasks = new List<Task>()
            //};
            //SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);
            //saveLoadSystemViewModel.Save(@"E:\zyadhamedashour\" + shedule.Name, shedule);

            if (FileManger.GetAllFilesByExtension(@"E:\zyadhamedashour", ".pmshed").Count < 1)
            {
                NoSheduleGrid.Visibility = Visibility.Visible;
            }
            else
            {
                tvTasks.Visibility = Visibility.Visible;
                FillTreeView();
            }



        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            //Create a CreatingItem Window with 2 Data Fields(The Task Title and The Task Duration)
            Create_ModifyItemsWindow CreateWindow = new Create_ModifyItemsWindow(2);
            //Set the First Label Text to Task Title
            CreateWindow.lbDataField1Text = "Task Title: ";
            //Set the Second Label Text to Task Duration
            CreateWindow.lbDataField3Text = "Task Duration: ";
            //Make the IsDatePicker Property Visible so that the Second Data Entry Is a DatePicker
            CreateWindow.IsDatePickerVisible = true;
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
            //MessageBox.Show((tvTasks.ItemContainerGenerator.ContainerFromItem(tvTasks.SelectedItem).GetType().ToString()));
            //List<TaskBlock> blocks = tviUpcomingTasks.ItemsSource as List<TaskBlock>;
            //MessageBox.Show(blocks[0].Task.TaskTitle);
            //MessageBox.Show((tvTasks.SelectedItem as TaskBlock).Task.TaskTitle);
        }

        #endregion

        #region Methods

        void FillTreeView()
        {
            //Set the ItemsSource of all the TreeNodes to null(to remove any Parents to the Items)
            tviUpcomingTasks.ItemsSource = null;
            tviInProgress.ItemsSource = null;
            tviDone.ItemsSource = null;
            tviUndone.ItemsSource = null;

            //Clear all the Items inside all the TreeNodes
            tviUpcomingTasks.Items.Clear();
            tviInProgress.Items.Clear();
            tviDone.Items.Clear();
            tviUndone.Items.Clear();

            //Reset the Items Source for each one of them
            tviUpcomingTasks.ItemsSource = sheduleMangerViewModel.UpcomingTasks;

            tviInProgress.ItemsSource = sheduleMangerViewModel.InProgressTasks;

            tviDone.ItemsSource = sheduleMangerViewModel.DoneTasks;

            tviUndone.ItemsSource = sheduleMangerViewModel.UndoneTasks;

        }

        #endregion

        #region Events

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            //If the Selected Item Was Not a Task Block, Don't do anything
            if ((tvTasks.SelectedItem as TaskBlock) != null)
            {
                //If it was a Task Block, Store it in a Variable
                TaskBlock SelectedItem = tvTasks.SelectedItem as TaskBlock;
                //Get the Operation that the User Selected using the MenuItem's name
                //If the User Selected Edit Task, Edit the Selected Task
                if ((e.Source as MenuItem).Name == "menuItemEditTask")
                {
                    //Create a CreatingItem Window with 2 Data Fields(The Task Title and The Task Duration)
                    Create_ModifyItemsWindow CreateWindow = new Create_ModifyItemsWindow(2);

                    //Set the First Label Text to Task Title
                    CreateWindow.lbDataField1Text = "Task Title: ";

                    //Set the Second Label Text to Task Duration
                    CreateWindow.lbDataField3Text = "Task Duration: ";

                    //Make the IsDatePicker Property Visible so that the Second Data Entry Is a DatePicker
                    CreateWindow.IsDatePickerVisible = true;

                    //Set the Title of the Window to Edit Task
                    CreateWindow.Title = "Edit Task";

                    //Set the Text Inside the First TextBox to The Text of the Task Title
                    CreateWindow.txtDataField1Text = SelectedItem.Task.Title;

                    //Set the Start Date and the End Date Inside the Window to the Start and End Dates of the Task
                    CreateWindow.StartDate = SelectedItem.Task.StartDate;
                    CreateWindow.EndDate = SelectedItem.Task.EndDate;

                    //Show the Window as Dialog
                    if (CreateWindow.ShowDialog() == true)
                    {
                        //Set the Data of the Task to the New Data in the Window
                        SelectedItem.Task.Title = CreateWindow.txtDataField1Text;
                        SelectedItem.Task.StartDate = CreateWindow.StartDate;
                        SelectedItem.Task.EndDate = CreateWindow.EndDate;

                        //Save the New Data of the Shedule(To save the changes Made in the Task)
                        sheduleMangerViewModel.SaveSheduleData();

                        //Refill the TreeView to see the Changes
                        FillTreeView();
                    }

                }
                //If the User Selected Delete Task, Delete the Selected Task
                else if ((e.Source as MenuItem).Name == "menuItemDeleteTask")
                {
                    //Remove the Task that is inside the TaskBlock from the Shedule
                    sheduleMangerViewModel.Shedule.Tasks.Remove(SelectedItem.Task);

                    //Save the New Data in the Shedule
                    sheduleMangerViewModel.SaveSheduleData();

                    //Refill the TreeView to see the Changes
                    FillTreeView();
                }
                //If the User Selected Mark Task As Done, Mark the Selected Task as Done
                else if ((e.Source as MenuItem).Name == "menuItemMarkTask")
                {
                    //Set the Stored Task in the TaskBlock as Done
                    SelectedItem.Task.IsDone = true;

                    //Save the New Data of the Shedule(To save the changes Made in the Task)
                    sheduleMangerViewModel.SaveSheduleData();

                    //Refill the TreeView to see the Changes
                    FillTreeView();
                }
            }

        }

        #endregion

    }
}
