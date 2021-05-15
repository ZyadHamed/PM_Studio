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
        StageMangerViewModel stageMangerViewModel;
        public StagesManger()
        {
            InitializeComponent();
            SaveLoadSystemViewModel sv = new SaveLoadSystemViewModel(null);
            sv.CreateStagesFile(@"E:\", "Stages2.pmstage");
            stageMangerViewModel = new StageMangerViewModel(@"E:\Stages2.pmstage");

            //This Sample code was used to test the AddStage method
            List<Feature> features = new List<Feature>() { new Feature() { Header = "Bitfrost Caching", Description = "Automatic Caching for bitfrost FX" }, 
            new Feature() { Header = "Ram cooking", Description = "Eating Ram as Chrome tbh" },
            new Feature() { Header = "Empty", Description = "Nothing" }};

            List<BugToFix> bugs = new List<BugToFix>() { new BugToFix() { Header = "Bitfrost Caching", Description = "No Caching for bitfrost FX" },
            new BugToFix() { Header = "bad Ram eating", Description = "Bad Ram as Chrome tbh" },
            new BugToFix() { Header = "Empty2", Description = "Nothing" }};
            stageMangerViewModel.AddStage(new Stage
            {
                StageType = "In Progress",
                Version = "1.2.5",
                StartDate = DateTime.Parse("5/25/2021"),
                EndDate = DateTime.Parse("6/25/2021"),
                Features = features,
                BugsToFix = bugs
            });
            UpdateListViews();
        }

        void UpdateListViews()
        {
            
            lstUpcomingVerticalView.ItemsSource = stageMangerViewModel.UpcomingStages;
            lstInProgressVerticalView.ItemsSource = stageMangerViewModel.InProgressStages;
            lstReleasedVerticalView.ItemsSource = stageMangerViewModel.DoneStages;
        }

        private void lst_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StageBlock selectedStage = (sender as ListView).SelectedItem as StageBlock;
            ContentPresenter presenter = this.VisualParent as ContentPresenter;
            (presenter.TemplatedParent as Frame).Content = new StagePage(selectedStage.Stage);
            
        }
    }
}
