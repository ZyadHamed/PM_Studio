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
        public MainWindow()
        {
            InitializeComponent();
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

    }
}
