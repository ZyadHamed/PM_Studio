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
    /// Interaction logic for StagesManger.xaml
    /// </summary>
    public partial class StagesManger : Page
    {
        StageMangerViewModel stageMangerViewModel = new StageMangerViewModel(@"E:\Stages2.pmstage");
        public StagesManger()
        {
            InitializeComponent();

            //This Sample code was used to test the AddStage method
            //stageMangerViewModel.AddStage(new Stage 
            //{
            //    StageType = "In Progress",
            //    Version = "1.2.5",
            //    StartDate = DateTime.Parse("5/25/2021"),
            //    EndDate = DateTime.Parse("6/25/2021")
            //});
            UpdateListViews();
        }

        void UpdateListViews()
        {
            
            lstUpcomingVerticalView.ItemsSource = stageMangerViewModel.UpcomingStages;
            lstInProgressVerticalView.ItemsSource = stageMangerViewModel.InProgressStages;
            lstReleasedVerticalView.ItemsSource = stageMangerViewModel.DoneStages;
        }

    }
}
