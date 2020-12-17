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
        public StagesManger()
        {
            InitializeComponent();
            lstReleasedVerticalView.Items.Add(new StageBlock(new Stage() 
            {
                StageType = "Beta",
                Version = "1.2.5",
                StartDate = DateTime.Parse("5/25/2021"),
                EndDate = DateTime.Parse("6/25/2021")
            }));
        }
    }
}
