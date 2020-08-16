using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PM_Studio
{
    class ImagelistItem : StackPanel
    {
        public string itemText;

        #region Constructor
        /// <summary>
        /// This Class Inherts from StackPanel class
        /// It was made to be used as icon&name display for files and folders in the file explorer
        /// It is populated with data then used in a ListView
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        public ImagelistItem(string iconPath, string text)
        {
            //Make a Bitmap Image to be the icon using the given path
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(iconPath, UriKind.Relative);
            bitmap.DecodePixelWidth = 30;
            bitmap.DecodePixelHeight = 30;
            bitmap.EndInit();

            itemText = text;

            Orientation = Orientation.Horizontal;
            //Add the Image to the StackPanel
            Children.Add(new Image
            {
                Source = bitmap
            });
            //Add the Text to the StackPanel
            Children.Add(new TextBlock
            {
                Text = text,

            });
        }
        #endregion

    }
}
