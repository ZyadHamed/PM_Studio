using System;
using System.Collections.Generic;
using System.Globalization;
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
            //    Name = "PMShedule2.pmshed",
            //    Tasks = tasks
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
            if(CreateWindow.ShowDialog() == true)
            {
                //If the User Has Pressed the OK button, Add The Task
                //Create a string holding the 2 Dates in dd/M/yyyy Form(ex: 22/9/2020 To 18/2/2021)
                string Date = CreateWindow.StartDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " To " + CreateWindow.EndDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                //Add a Task using the Data inside the Window
                sheduleMangerViewModel.AddTask(new Task(CreateWindow.txtDataField1Text, "", Date)
                {
                    TaskProgress = "Undone"
                });
                //Reload the Items of the Tree View
                FillTreeView();
            }
            
        }

        #endregion

        #region Methods

        void FillTreeView()
        {
            //Set the ItemsSource of all the TreeNodes to null(to remove any Parents to the Items)
            tviUndone.ItemsSource = null;
            tviInProgress.ItemsSource = null;
            tviDone.ItemsSource = null;

            //Clear all the Items inside all the TreeNodes
            tviUndone.Items.Clear();
            tviInProgress.Items.Clear();
            tviDone.Items.Clear();

            //Reset the Items Source for each one of them
            tviUndone.ItemsSource = sheduleMangerViewModel.UndoneTasks;

            tviInProgress.ItemsSource = sheduleMangerViewModel.InProgressTasks;
            
            tviDone.ItemsSource = sheduleMangerViewModel.DoneTasks;
        }

        #endregion
    }
}
