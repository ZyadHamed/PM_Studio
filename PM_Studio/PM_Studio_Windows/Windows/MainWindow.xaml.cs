using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            btnAlgorithm.IsChecked = true;
            
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            int buttonIndex = int.Parse(((ToggleButton)e.Source).Uid);
            switch (buttonIndex)
            {
                case 0:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new AlgorithmEditor();
                    break;

                case 1:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new MarketPlace();
                    break;

                case 2:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new SheduleManger();
                    break;

                case 3:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new TeamManger();
                    break;

                case 4:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new StagesManger();
                    break;

                case 5:
                    PagesContainer.Content = null;
                    PagesContainer.Content = new PublishManger();
                    break;
            }

            foreach(var Control in LeftPanelGrid.Children)
            {
                if(Control.GetType() == typeof(ToggleButton))
                {
                    ToggleButton button = Control as ToggleButton;
                    if (button == ((ToggleButton)e.Source))
                    {
                        button.IsChecked = true;
                    }
                    else
                    {
                        button.IsChecked = false;
                    }
                }
               
            }
        }

        private void btnCollapsePanel_Click(object sender, RoutedEventArgs e)
        {
            //If the Panel was not collapsed, clear the text of the buttons, and make the buttons panel has auto width
            //and make the frames panel take the rest space(collapsing the panel)
            //then mark the IsPanelCollapsed as true
            if(IsPanelCollapsed == false)
            {
                lbAlgorithmText.Text = "";
                lbMarketPlaceText.Text = "";
                lbSheduleText.Text = "";
                lbTeamText.Text = "";
                lbStagesText.Text = "";
                lbPublishText.Text = "";
                TabButtonsColumn.Width = new GridLength(1, GridUnitType.Auto);
                TabesColumn.Width = new GridLength(1, GridUnitType.Star);
                btnCollapsePanel.Content = ">";
                IsPanelCollapsed = true;
            }
            //If the panel was collapsed, set the buttons text to the original text
            //and reset the panels back to their orginal width
            //then mark the IsPanelCollapsed as false
            else if(IsPanelCollapsed == true)
            {
                lbAlgorithmText.Text = "Algorithm";
                lbMarketPlaceText.Text = "Market Place";
                lbSheduleText.Text = "Schedule";
                lbStagesText.Text = "Stages";
                lbTeamText.Text = "Team";
                lbPublishText.Text = "Publish";
                TabButtonsColumn.Width = new GridLength(1, GridUnitType.Star);
                TabesColumn.Width = new GridLength(4, GridUnitType.Star);
                btnCollapsePanel.Content = "<";
                IsPanelCollapsed = false;
            }
        }
    }
}
