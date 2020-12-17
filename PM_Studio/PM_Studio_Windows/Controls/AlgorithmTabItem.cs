using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PM_Studio
{
    class AlgorithmTabItem : FileTabItem
    {
        #region Variables
        int undocount = 0;
        public int count = 1;
        Algorithm algorithm;

        List<string> LastData = new List<string>();

        System.Windows.Forms.RichTextBox rtxtAlgorithm = new System.Windows.Forms.RichTextBox();

        TextFormatter textFormatter = new TextFormatter();
        #endregion

        #region Constructor
        public AlgorithmTabItem(TabControl tabControl, Algorithm _algorithm, string filePath = "", bool AddbyDefault = false) : base(tabControl, _algorithm.algorithmFileName.Replace("*", ""), filePath)
        {
            this.tabControl = tabControl;

            //assign the local algorithm to the incoming algorithm
            algorithm = _algorithm;

            //Set the properties of the RichTextBox
            rtxtAlgorithm.Dock = System.Windows.Forms.DockStyle.Fill;
            rtxtAlgorithm.TextChanged += rtxtAlgorithm_TextChanged;
            rtxtAlgorithm.KeyDown += rtxtAlgorithm_KeyDown;
            rtxtAlgorithm.Font = new System.Drawing.Font("Arial", 15);
            rtxtAlgorithm.WordWrap = false;
            rtxtAlgorithm.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            rtxtAlgorithm.BackColor = System.Drawing.Color.FromArgb(13, 3, 19);
            rtxtAlgorithm.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
            rtxtAlgorithm.Text = algorithm.algorithm;
            
            //Define a windows forms host to handle the Windows Forms Control(RichTextBox)
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
            host.Child = rtxtAlgorithm;
            //Add the RichTextBox to the tab
            this.AddChild(host);
            //Add the tab to the tabControl if the AddByDefault was true
            if(AddbyDefault == true)
            {
                tabControl.Items.Add(this);
                tabControl.SelectedItem = this;
            }

            //Format the Text Inside the AlgorithmTextBox
            textFormatter.FormatAlgorithmTextBox(rtxtAlgorithm, @"(\[\d*\])");

            SaveFile();
            
        }
        #endregion
        
        #region Events
        private void rtxtAlgorithm_TextChanged(object sender, EventArgs e)
        {
            //Check if the file is saved
            //If it is, mark it as unsaved
            if (IsSaved == true)
            {
                IsSaved = false;
            }
           
            //Unfocus the richtextbox to avoid blinking
            this.Focus();

            //Then Format all the text again and restore the focus
            textFormatter.FormatAlgorithmTextBox(rtxtAlgorithm, @"(\[\d*\])");

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

            else if(e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                int CurrentLine = rtxtAlgorithm.GetLineFromCharIndex(rtxtAlgorithm.SelectionStart);
                if(Regex.Replace(rtxtAlgorithm.Lines[CurrentLine], @"(\[\d*\])", "") == "")
                {
                    int OldSelectionStart = rtxtAlgorithm.SelectionStart;
                    int OldSelectionLength = rtxtAlgorithm.SelectionLength;
                    rtxtAlgorithm.SelectionStart = rtxtAlgorithm.GetFirstCharIndexFromLine(CurrentLine) - 1;
                    rtxtAlgorithm.SelectionLength = rtxtAlgorithm.Lines[CurrentLine].Length;
                    rtxtAlgorithm.SelectedText = String.Empty;
                    rtxtAlgorithm.SelectionStart = OldSelectionStart;
                    rtxtAlgorithm.SelectionLength = OldSelectionLength;
                }
            }
           
            else if (e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                LastData.Add(rtxtAlgorithm.Text);
                undocount = 0;
            }

        }

        #endregion

        #region Methods

        public override void SaveFile()
        {
            //Get the current path of the file
            string CurrentPath = FilePath;

            //Make a new algorithm class based on the new data in the file
            Algorithm algorithm = new Algorithm()
            {
                algorithm = rtxtAlgorithm.Text,
                algorithmFileName = HeaderText
            };

            //Save the File
            saveLoadSystemViewModel.Save(CurrentPath, algorithm);

            //Mark IsSaved as true
            IsSaved = true;
        }

        #endregion

    }
}
