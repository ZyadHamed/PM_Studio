using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PM_Studio
{
    class FileTabItem : TabItem
    {
        #region Variables
        public bool IsSaved = true;

        public StackPanel tabHeader = new StackPanel();
        public TextBlock headerText = new TextBlock();
        public Button closeButton = new Button();
        public TabControl tabControl;

        #endregion

        #region Constructor
        public FileTabItem(TabControl TabControl, string Header = "", string FilePath = "")
        {
            //Set the properties of the closing button
            closeButton.Content = "X";
            closeButton.BorderThickness = new System.Windows.Thickness(0);

            //Set the header text to the incoming text, and add the closing button
            headerText.Text = Header;
            tabHeader.Orientation = Orientation.Horizontal;
            tabHeader.Children.Add(headerText);
            tabHeader.Children.Add(closeButton);

            //Set the header of the tab to the incoming header, and the tag to the file path
            this.Header = tabHeader;
            this.Tag = FilePath;


        }

      
        #endregion

    }
}
