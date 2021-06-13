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
    /// Interaction logic for MarketPlace.xaml
    /// </summary>
    /// 
    
    //Yo!, this code will be updated once I afford a server to upload the files on it :)
    public partial class MarketPlace : Page
    {
        List<MarketPlaceItem> items;
        int lastStopIndex = 0;
        public MarketPlace()
        {
            InitializeComponent();
            //string imagePath = "pack://application:,,,/PM_Studio_Windows;component/Images/Algorithm.png";
            //MarketPlaceItem item = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed1");
            //MarketPlaceItem item2 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed2");
            //MarketPlaceItem item3 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed3");
            //MarketPlaceItem item4 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed4");
            //MarketPlaceItem item5 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed5");
            //MarketPlaceItem item6 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed6");
            //MarketPlaceItem item7 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed7");
            //MarketPlaceItem item8 = new MarketPlaceItem(imagePath, "My Algorithm", "Zyad Hamed8");
            //items = new List<MarketPlaceItem> { item, item2, item3, item4, item5, item6, item7, item8 };
            FileUploader uploader = new FileUploader();
            uploader.UploadFile();
            MessageBox.Show("File is Uploaded");
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(lastStopIndex <= 0)
            {
                MessageBox.Show("No More previous");
            }
            else
            {
                wpAlgorithms.Children.Clear();
                for (int i = lastStopIndex - 3; i < lastStopIndex; i++)
                {
                    wpAlgorithms.Children.Add(items[i]);
                }
                lastStopIndex -= 3;

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lastStopIndex >= items.Count - 1)
            {
                MessageBox.Show("No More next");
            }
            else
            {
                wpAlgorithms.Children.Clear();
                for (int i = lastStopIndex; i < lastStopIndex + 3; i++)
                {
                    wpAlgorithms.Children.Add(items[i]);
                }
                lastStopIndex += 3;
                
            }
        }
    }
}
