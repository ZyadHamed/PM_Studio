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
    /// Interaction logic for SheduleManger.xaml
    /// </summary>
    public partial class SheduleManger : Page
    {

       

        #region Constructor
        public SheduleManger()
        {
            InitializeComponent();

            ////Code Used for Creating a shedule file
            //Task task1 = new Task("Design the Form", "", "From 9/9/2020 To 15/12/2020");
            //task1.TaskProgress = "Done";
            //Task task2 = new Task("Program the front End of the Form", "", "From 8/8/2020 To 8/9/2020");
            //task2.TaskProgress = "Done";
            //Task task3 = new Task("Revise the Code", "", "From 9/10/2020 To 5/11/2020");
            //task3.TaskProgress = "In Progress";
            //Task task4 = new Task("Fix Bugs", "", "From 12/12/2020 To 5/1/2021");
            //task4.TaskProgress = "Undone";
            //Task task5 = new Task("Publish the Program", "", "From 5/1/2021 To 15/1/2021");
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
            //    Tasks = tasks
            //};
            //SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel(null);
            //saveLoadSystemViewModel.Save((@"E:\zyadhamedashour\" + shedule.Name, shedule));
            SheduleMangerViewModel sheduleMangerViewModel = new SheduleMangerViewModel(@"E:\zyadhamedashour\PMShedule.pmshed");

            tviUndone.ItemsSource = sheduleMangerViewModel.UndoneTasks;
            tviInProgress.ItemsSource = sheduleMangerViewModel.InProgressTasks;
            tviDone.ItemsSource = sheduleMangerViewModel.DoneTasks;

        }

        #endregion

    }
}
