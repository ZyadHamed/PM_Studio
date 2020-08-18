using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    class IdeaTabItem : TabItem
    {
        #region Variables
        int undocount = 0;
        public int count = 1;
        public bool IsSaved = true;

        List<string> LastData = new List<string>();

        System.Windows.Forms.RichTextBox rtxtIdea = new System.Windows.Forms.RichTextBox();
        StackPanel tabHeader = new StackPanel();
        TextBlock headerText = new TextBlock();
        Button closeButton = new Button();
        public TabControl tabControl;

        #endregion

        #region Constructor
        public IdeaTabItem(TabControl tabControl, string AlgorithmText = "", string header = "", string filePath = "", bool AddbyDefault = false)
        {
            this.tabControl = tabControl;

            //Set the properties of the RichTextBox
            rtxtIdea.Dock = System.Windows.Forms.DockStyle.Fill;
            rtxtIdea.TextChanged += rtxtIdea_TextChanged;
            rtxtIdea.KeyDown += rtxtIdea_KeyDown;
            rtxtIdea.Font = new System.Drawing.Font("Arial", 15);
            rtxtIdea.WordWrap = false;
            rtxtIdea.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            rtxtIdea.BackColor = System.Drawing.Color.FromArgb(13, 3, 19);
            rtxtIdea.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            rtxtIdea.Text = AlgorithmText;



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
            host.Child = rtxtIdea;
            //Add the RichTextBox to the tab
            this.AddChild(host);
            //Add the tab to the tabControl if the AddByDefault was true
            if (AddbyDefault == true)
            {
                tabControl.Items.Add(this);
                tabControl.SelectedItem = this;
            }


        }
        #endregion

        #region Events
        private void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void rtxtIdea_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void rtxtIdea_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
