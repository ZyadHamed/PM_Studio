using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PM_Studio
{
    class MarketPlaceItem : StackPanel
    {

        #region Designing Variables

        Image img = new Image();
        TextBlock lbItemTitle = new TextBlock();
        TextBlock lbItemCreator = new TextBlock();

        #endregion

        #region Constructor

        public MarketPlaceItem(string imagePath, string itemTitle, string itemCreator)
        {
            ImagePath = imagePath;
            ItemTitle = itemTitle;
            ItemCreator = itemCreator;
            SetControlsProperties();
            SetControlsData();
        }

        #endregion

        #region DesigningMethods

        void SetControlsProperties()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ImagePath);
            bitmap.DecodePixelWidth = 30;
            bitmap.DecodePixelHeight = 30;
            bitmap.EndInit();
            img.Source = bitmap;
            img.Width = 100;

            lbItemTitle.Foreground = Brushes.GhostWhite;
            lbItemCreator.Foreground = Brushes.GhostWhite;

            lbItemTitle.FontSize = 20;
            lbItemCreator.FontSize = 15;

            this.Margin = new System.Windows.Thickness(15);

            this.Children.Add(img);
            this.Children.Add(lbItemTitle);
            this.Children.Add(lbItemCreator);
        }

        #endregion

        #region Methods

        void SetControlsData()
        {
            lbItemTitle.Text = ItemTitle;
            lbItemCreator.Text = ItemCreator;
        }

        #endregion

        #region Properties

        public string ImagePath { get; set; }
        public string ItemTitle { get; set; }
        public string ItemCreator { get; set; }

        #endregion

    }
}
