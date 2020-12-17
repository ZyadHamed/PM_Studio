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
        public StagePage()
        {
            InitializeComponent();
            FeatureBlock block1 = new FeatureBlock(new Feature() { Header = "Bitfrost Caching" , Description = "Automatic Caching for bitfrost FX"});
            FeatureBlock block2 = new FeatureBlock(new Feature() { Header = "Bitfrost Caching", Description = "Eating Ram as Chrome tbh" });
            FeatureBlock block3 = new FeatureBlock(new Feature() { Header = "Bitfrost Caching", Description = "Nothing" });
            FeatureBlock block4 = new FeatureBlock(new Feature() { Header = "Bitfrost Caching", Description = "" });
            List<FeatureBlock> blocks = new List<FeatureBlock>() { block1, block2, block3, block4 };
            FeaturesHeader header = new FeaturesHeader("Modeling");
            FeaturesHeader header2 = new FeaturesHeader("Animation");
            FeaturesHeader header3 = new FeaturesHeader("Rigging");
            FeaturesHeader header4 = new FeaturesHeader("Texturing");
            header.ItemsSource = blocks;
            header2.ItemsSource = blocks;
            header3.ItemsSource = blocks;
            header4.ItemsSource = blocks;
            tvFeatures.Items.Add(header);
            tvFeatures.Items.Add(header2);
            tvFeatures.Items.Add(header3);
            tvFeatures.Items.Add(header4);
        }
    }
}
