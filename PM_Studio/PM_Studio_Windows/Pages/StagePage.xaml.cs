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
    /// Interaction logic for StagePage.xaml
    /// </summary>
    public partial class StagePage : Page
    {
        public StagePage(Stage stage)
        {
            InitializeComponent();
            Stage = stage;
            List<FeatureBlock> FeatureBlocks = GetFeatureBlocks();
            List<BugsBlock> BugBlocks = GetBugBlocks();
            lstFeatures.ItemsSource = FeatureBlocks;
            lstBugsToFix.ItemsSource = BugBlocks;
        }

        #region Methods

        List<FeatureBlock> GetFeatureBlocks()
        {
            List<FeatureBlock> featureBlocks = new List<FeatureBlock>();
            foreach(Feature feature in Stage.Features)
            {
                FeatureBlock featureBlock = new FeatureBlock(feature);
                featureBlocks.Add(featureBlock);
            }
            return featureBlocks;
        }

        List<BugsBlock> GetBugBlocks()
        {
            List<BugsBlock> bugBlocks = new List<BugsBlock>();
            foreach (BugToFix bug in Stage.BugsToFix)
            {
                BugsBlock bugBlock = new BugsBlock(bug);
                bugBlocks.Add(bugBlock);
            }
            return bugBlocks;
        }

        #endregion

        #region Properties

        public Stage Stage { get; set; }

        #endregion

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ContentPresenter presenter = this.VisualParent as ContentPresenter;
            (presenter.TemplatedParent as Frame).Content = new StagesManger();
        }
    }
}
