using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PM_Studio
{
    class AlgorithmTabItem : TabItem
    {
        #region Variables
        int undocount = 0;
        public int count = 1;
        public bool IsSaved = true;

        List<string> LastData = new List<string>();

        System.Windows.Forms.RichTextBox rtxtAlgorithm = new System.Windows.Forms.RichTextBox();
        StackPanel tabHeader = new StackPanel();
        TextBlock headerText = new TextBlock();
        Button closeButton = new Button();
        TabControl tabControl;

        #endregion


        #region Constructor
        public AlgorithmTabItem(TabControl tabControl, string incomingString = "", string header = "", string filePath = "")
        {
            this.tabControl = tabControl;

            //Set the properties of the RichTextBox
            rtxtAlgorithm.Dock = System.Windows.Forms.DockStyle.Fill;
            rtxtAlgorithm.TextChanged += rtxtAlgorithm_TextChanged;
            rtxtAlgorithm.KeyDown += rtxtAlgorithm_KeyDown;
            rtxtAlgorithm.Font = new System.Drawing.Font("Arial", 15);
            rtxtAlgorithm.WordWrap = false;
            rtxtAlgorithm.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            rtxtAlgorithm.BackColor = System.Drawing.Color.FromArgb(13, 3, 19);
            rtxtAlgorithm.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            rtxtAlgorithm.Text = incomingString;



            //Set the properties of the closing button
            closeButton.Content = "X";
            closeButton.BorderThickness = new System.Windows.Thickness(0);
            //closeButton.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(1, 210, 210, 210));
            closeButton.Click += closeButton_Click;

            //Set the header text to the incoming text, and add the closing button
            headerText.Text = header;
            tabHeader.Orientation = Orientation.Horizontal;
            tabHeader.Children.Add(headerText);
            tabHeader.Children.Add(closeButton);


            //Set the header of the tab to the incoming header, and the tag to the file path
            this.Header = tabHeader;
            this.Tag = filePath;

            //Define a windows forms host to handle the Windows Forms Control(RichTextBox)
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
            host.Child = rtxtAlgorithm;
            //Add the RichTextBox to the tab
            this.AddChild(host);
            //Add the tab to the tabControl
            tabControl.Items.Add(this);
            tabControl.SelectedItem = this;

        }
        #endregion

        #region Events
        private void rtxtAlgorithm_TextChanged(object sender, EventArgs e)
        {
            //Check if the file is saved
            //If it is, mark it as unsaved and add the unsaved star to the header
            if (IsSaved == true)
            {
                ((TextBlock)tabHeader.Children[0]).Text += "*";
                IsSaved = false;
            }

            //Unfocus the richtextbox to avoid blinking
            this.Focus();
            //Then Format all the text again and restore the focus
            //TextFormatter textFormatter = new TextFormatter();
            //textFormatter.FormatAlgorithmTextBox(rtxtAlgorithm, @"(\[\d*\])");
        }

        private void rtxtAlgorithm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {


            //check what the user had pressed
            //If he Pressed Ctrl+S, save the file
            if (System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control && e.KeyCode == System.Windows.Forms.Keys.S)
            {
                //Check if the file is already saved
                //If not,Save it
                if (IsSaved == false)
                {
                    SaveFile();
                }

            }

            //If he Pressed Ctrl + Z, undo
            else if (System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control && e.KeyCode == System.Windows.Forms.Keys.Z)
            {
                if (LastData.Count - undocount - 1 > -1)
                {
                    rtxtAlgorithm.Text = LastData[LastData.Count - undocount - 1];
                    ++undocount;
                }
            }

            else if (System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control && e.KeyCode == System.Windows.Forms.Keys.B)
            {
                rtxtAlgorithm.SelectionBullet = true;
            }

            else if (e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                LastData.Add(rtxtAlgorithm.Text);
                undocount = 0;
            }

        }

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

        #region Methods
        public void SaveFile()
        {
            //Get the current path of the file,(was saved before in the tab tag)
            string CurrentPath = this.Tag.ToString();

            //Make an instance of the Project Saving Class
            SaveLoadSystemViewModel sp = new SaveLoadSystemViewModel();

            //Make a new algorithm class based on the new data in the file
            Algorithm algorithm = new Algorithm()
            {
                algorithm = rtxtAlgorithm.Text,
                algorithmFileName = ((TextBlock)tabHeader.Children[0]).Text

            };
            //Mark IsSaved as true
            IsSaved = true;
            //Save the File
            sp.Save(CurrentPath, algorithm);
            //Remove the unsaved star from the header
            ((TextBlock)tabHeader.Children[0]).Text = ((TextBlock)tabHeader.Children[0]).Text.Remove(((TextBlock)tabHeader.Children[0]).Text.Length - 1);
        }
        #endregion

    }
}
