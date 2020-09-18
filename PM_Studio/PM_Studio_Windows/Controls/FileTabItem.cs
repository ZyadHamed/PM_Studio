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


        public SaveLoadSystemViewModel saveLoadSystemViewModel;

        public StackPanel tabHeader = new StackPanel();
        public TextBlock headerText = new TextBlock();
        public Button closeButton = new Button();
        public TabControl tabControl;

        #endregion

        #region Constructor
        public FileTabItem(TabControl TabControl, string Header = "", string FilePath = "")
        {
            //Set the tabControl field to the incoming TabControl
            tabControl = TabControl;
            //Set the tabControl Paramerter of the SaveLoadSystemViewModel to tabControl
            saveLoadSystemViewModel = new SaveLoadSystemViewModel(tabControl);
            //Set the properties of the closing button
            closeButton.Content = "X";
            closeButton.BorderThickness = new System.Windows.Thickness(0);

            //Add the click event to the closing button
            closeButton.Click += closeButton_Click;

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

        #region Methods
        public virtual void SaveFile()
        {

        }
        #endregion

        #region Events
        private void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Check If value of IsSaved is false
            //If yes, then the file isn't saved yet
            if (IsSaved == false)
            {
                //Ask the user wheather to save this file before closing or not
                if (MessageBox.Show("File \"" + ((TextBlock)tabHeader.Children[0]).Text + "\" Hasn't been saved yet\n Do you Want to Save it Before closing?", "Save file before close", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //If yes, save the file and remove the tab
                    SaveFile();
                    tabControl.Items.Remove(this);
                }
                //If not, Quit without saving
                else
                {
                    tabControl.Items.Remove(this);
                }
            }
            //If the file Is already saved, Close the tab
            else
            {
                tabControl.Items.Remove(this);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Text of the Header of the TabItem
        /// </summary>
        public string HeaderText
        {
            get
            {
                return headerText.Text;
            }
            set
            {
                headerText.Text = value;
            }
        }

        #endregion

    }
}
