using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsPanelCollapsed = false;
        public MainWindow()
        {
            InitializeComponent();
            PagesContainer.Content = new AlgorithmEditor();
            
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            int buttonIndex = int.Parse(((Button)e.Source).Uid);
            switch (buttonIndex)
            {
                case 0:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new AlgorithmEditor();
                    break;
                case 1:
                  
                    break;

                case 2:
                 
                    break;
                case 3:
                    
                    break;
            }
        }

        private void btnCollapsePanel_Click(object sender, RoutedEventArgs e)
        {
            //If the Panel was not collapsed, clear the text of the buttons, and make the buttons panel has auto width
            //and make the frames panel take the rest space(collapsing the panel)
            //then mark the IsPanelCollapsed as true
            if(IsPanelCollapsed == false)
            {
                btnAlgorithm.Content = "A";
                btnSchedule.Content = "S";
                btnPublish.Content = "P";
                btnTeam.Content = "T";
                TabButtonsColumn.Width = new GridLength(1, GridUnitType.Auto);
                TabesColumn.Width = new GridLength(1, GridUnitType.Star);
                IsPanelCollapsed = true;
            }
            //If the panel was collapsed, set the buttons text to the original text
            //and reset the panels back to their orginal width
            //then mark the IsPanelCollapsed as false
            else if(IsPanelCollapsed == true)
            {
                btnAlgorithm.Content = "Algorithm";
                btnSchedule.Content = "Schedule";
                btnPublish.Content = "Publish";
                btnTeam.Content = "Team";
                TabButtonsColumn.Width = new GridLength(1, GridUnitType.Star);
                TabesColumn.Width = new GridLength(4, GridUnitType.Star);
                IsPanelCollapsed = false;
            }
        }
    }
}
