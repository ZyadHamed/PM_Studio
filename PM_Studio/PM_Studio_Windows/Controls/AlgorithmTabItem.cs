using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PM_Studio
{
    class AlgorithmTabItem : FileTabItem
    {
        #region Designing Variables

        System.Windows.Forms.RichTextBox rtxtAlgorithm = new System.Windows.Forms.RichTextBox();
        Button btnConvertAlgorithmToNodeSystem = new Button();

        #endregion

        #region Variables

        int undocount = 0;
        public int count = 1;
        List<string> LastData = new List<string>();

        Algorithm algorithm;
        TextFormatter textFormatter = new TextFormatter();

        #endregion

        #region Constructor

        public AlgorithmTabItem(TabControl tabControl, Algorithm _algorithm, string filePath = "", bool AddbyDefault = false) : base(tabControl, _algorithm.algorithmFileName.Replace("*", ""), filePath)
        {
            this.tabControl = tabControl;

            //assign the local algorithm to the incoming algorithm
            algorithm = _algorithm;

            SetControlsProperties();

            //Define a windows forms host to handle the Windows Forms Control(RichTextBox)
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
            host.Child = rtxtAlgorithm;

            //Add the RichTextBox to the Container Grid
            Container.Children.Add(host);
            Grid.SetRow(host, 1);

            //Add the tab to the tabControl if the AddByDefault was true
            if (AddbyDefault == true)
            {
                tabControl.Items.Add(this);
                tabControl.SelectedItem = this;
            }

            //Format the Text Inside the AlgorithmTextBox
            textFormatter.FormatAlgorithmTextBox(rtxtAlgorithm, @"(\[\d*\])");

            SaveFile();

        }

        #endregion

        #region Designing Methods

        void SetControlsProperties()
        {
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

            //Set the properties of the Converting Button
            btnConvertAlgorithmToNodeSystem.Content = "C";
            btnConvertAlgorithmToNodeSystem.Width = 30;
            btnConvertAlgorithmToNodeSystem.Height = 30;
            btnConvertAlgorithmToNodeSystem.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2D2D30"));
            btnConvertAlgorithmToNodeSystem.Foreground = System.Windows.Media.Brushes.WhiteSmoke;
            btnConvertAlgorithmToNodeSystem.Click += btnConvertAlgorithmToNodeSystem_Click;

            buttonsBar.Children.Add(btnConvertAlgorithmToNodeSystem);
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

            else if (e.KeyCode == System.Windows.Forms.Keys.Back)
            {
                if (rtxtAlgorithm.Text.Length > 0)
                {
                    int CurrentLine = rtxtAlgorithm.GetLineFromCharIndex(rtxtAlgorithm.SelectionStart);
                    if (Regex.Replace(rtxtAlgorithm.Lines[CurrentLine], @"(\[\d*\])", "") == "")
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


            }

            else if (e.KeyCode == System.Windows.Forms.Keys.Space)
            {
                LastData.Add(rtxtAlgorithm.Text);
                undocount = 0;
            }

        }

        private void btnConvertAlgorithmToNodeSystem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Create_ModifyItemsWindow window = new Create_ModifyItemsWindow(1);
            window.lbDataField1Text = "NodeSystem Name: ";

            if(window.ShowDialog() == true)
            {
                List<Node> nodes = new List<Node>();
                //Create a string instance which holds the algorithm text without the step indicators
                string textWithoutIndicators = Regex.Replace(rtxtAlgorithm.Text, @"(\[\d*\])", string.Empty);

                //Now create an array of strings each one contains one step from the algorithm
                string[] steps = textWithoutIndicators.Split("\n");

                //Loop inside each step in the array
                for (int i = 0; i < steps.Length; i++)
                {
                    //Create a node based on the text of that step
                    Node node = new Node();
                    node.Text = steps[i];

                    //Connect it to the next block if it exists
                    if (i + 1 < steps.Length)
                    {
                        node.ToNodeText = steps[i + 1];
                    }

                    //if the block was not the first block, set the block coordinates to the coordiantes of the previous block
                    //but with a space equal to half the space taken by the characters of the block
                    if(i - 1 >= 0)
                    {
                        node.X = nodes[i - 1].X + nodes[i - 1].Text.Length * 12;
                        node.Y = nodes[i - 1].Y;
                    }

                    //If the block was the first block, set the default coordiantes to it
                    else
                    {
                        node.X = 10;
                        node.Y = 40;
                    }

                    //Add the resulted node to the list
                    nodes.Add(node);
                }

                //Create a node system based on that list and the name that the user entered in the window
                NodeSystem nodeSystem = new NodeSystem();
                nodeSystem.fileName = window.txtDataField1Text + ".pmnodes";
                nodeSystem.Nodes = nodes;

                //Create a nodesystem file using that nodesystem
                saveLoadSystemViewModel.Save(@"E:\" + nodeSystem.fileName, nodeSystem);
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
