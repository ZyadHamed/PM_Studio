using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PM_Studio
{
    class StoryConceptsTabItem : FileTabItem
    {

        #region Variables
        Grid Container = new Grid();
        TextBox txtStoryType = new TextBox();
        TextBox txtStoryIdea = new TextBox();
        TextBox txtPlotTwists = new TextBox();
        TextBox txtPlotPoints = new TextBox();
        TextBox txtStoryEvents = new TextBox();
        TextBlock lbStoryType = new TextBlock();
        TextBlock lbStoryIdea = new TextBlock();
        TextBlock lbPlotTwists = new TextBlock();
        TextBlock lbPlotPoints = new TextBlock();
        TextBlock lbStoryEvents = new TextBlock();
        StoryConcepts storyConcepts;
        #endregion

        #region Constuctor
        public StoryConceptsTabItem(TabControl tabControl, string filePath, StoryConcepts StoryConcepts) : base(tabControl, StoryConcepts.fileName.Replace("*", ""), filePath)
        {
            //Assign the incoming StoryConcepts to the existing StoryConcepts
            storyConcepts = StoryConcepts;
            MessageBox.Show(storyConcepts.fileName);
            //Assign the events for all the textboxes
            txtStoryType.TextChanged += TextBox_TextChanged;
            txtStoryIdea.TextChanged += TextBox_TextChanged;
            txtPlotTwists.TextChanged +=TextBox_TextChanged;
            txtPlotPoints.TextChanged += TextBox_TextChanged;
            txtStoryEvents.TextChanged += TextBox_TextChanged;

            txtStoryType.KeyDown += TextBox_KeyDown;
            txtStoryIdea.KeyDown += TextBox_KeyDown;
            txtPlotTwists.KeyDown += TextBox_KeyDown;
            txtPlotPoints.KeyDown += TextBox_KeyDown;
            txtStoryEvents.KeyDown += TextBox_KeyDown;

            //Create the Rows and Columns for the grid
            AddGridRowsAndColumns();

            //Set the properties of the textblocks in the class
            SetTextBlocksProperties();

            //Set the properties of the textboxes in the class
            SetTextBoxesProperties();

            //Add all the controls to the grid
            AddControlsToGrid();

            //Oraganize the controls positions in the grid
            SetControlsPostions();

            //Add the Grid as a Child to this Tab Item
            this.AddChild(Container);

            //Fill the TextBoxes with the text from the storyConcepts File
            LoadTextBoxesText();

        }

        #endregion

        #region DesigningMethods
        /// <summary>
        /// Creates the Grid Rows and Columns where controls will be placed
        /// </summary>
        void AddGridRowsAndColumns()
        {
            //Create the 5 Grid Rows that will contain the 5 text fields
            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
            });

            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(2, System.Windows.GridUnitType.Star)
            });
            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(4, System.Windows.GridUnitType.Star)
            });
            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(4, System.Windows.GridUnitType.Star)
            });
            Container.RowDefinitions.Add(new RowDefinition()
            {
                Height = new System.Windows.GridLength(5, System.Windows.GridUnitType.Star)
            });

            //Create the 2 Columns that will contain the labels and the text fields
            Container.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Auto)
            });

            Container.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
            });
        }

        /// <summary>
        /// Sets the Properties for all the textblocks in the class
        /// </summary>
        void SetTextBlocksProperties()
        {
            //Set the text for each TextBlock
            lbStoryType.Text = "Story Type: ";
            lbStoryIdea.Text = "Idea of Story: ";
            lbPlotTwists.Text = "Plot Twists: ";
            lbPlotPoints.Text = "Plot Points: ";
            lbStoryEvents.Text = "Story Events In Details: ";

            //Set the ForeColor of all TextBlocks to white smoke
            lbStoryType.Foreground = Brushes.WhiteSmoke;
            lbStoryIdea.Foreground = Brushes.WhiteSmoke;
            lbPlotTwists.Foreground = Brushes.WhiteSmoke;
            lbPlotPoints.Foreground = Brushes.WhiteSmoke;
            lbStoryEvents.Foreground = Brushes.WhiteSmoke;

            //Set the font size for all TextBlocks
            lbStoryType.FontSize = 15;
            lbStoryIdea.FontSize = 15;
            lbPlotTwists.FontSize = 15;
            lbPlotPoints.FontSize = 15;
            lbStoryEvents.FontSize = 15;

            //Add some margin between the TextBlocks
            lbStoryIdea.Margin = new System.Windows.Thickness(0, 10, 0, 10);
            lbPlotTwists.Margin = new System.Windows.Thickness(0, 10, 0, 10);
            lbPlotPoints.Margin = new System.Windows.Thickness(0, 10, 0, 10);
            lbStoryEvents.Margin = new System.Windows.Thickness(0, 10, 0, 10);
        }

        /// <summary>
        /// Sets the properties of all TextBoxes in the Class
        /// </summary>
        void SetTextBoxesProperties()
        {
            //Set the font size for all TextBoxes
            txtStoryType.FontSize = 15;
            txtStoryIdea.FontSize = 15;
            txtPlotTwists.FontSize = 15;
            txtPlotPoints.FontSize = 15;
            txtStoryEvents.FontSize = 15;

            //Enable the multilines at all the TextBoxes
            txtStoryType.AcceptsReturn = true;
            txtStoryIdea.AcceptsReturn = true;
            txtPlotTwists.AcceptsReturn = true;
            txtPlotPoints.AcceptsReturn = true;
            txtStoryEvents.AcceptsReturn = true;

            //Set the ForeColor of all TextBoxes to white smoke
            txtStoryType.Foreground = Brushes.WhiteSmoke;
            txtStoryIdea.Foreground = Brushes.WhiteSmoke;
            txtPlotTwists.Foreground = Brushes.WhiteSmoke;
            txtPlotPoints.Foreground = Brushes.WhiteSmoke;
            txtStoryEvents.Foreground = Brushes.WhiteSmoke;

            //Set the BackColor of all TextBoxes to Dim Gray
            txtStoryType.Background = Brushes.DimGray;
            txtStoryIdea.Background = Brushes.DimGray;
            txtPlotTwists.Background = Brushes.DimGray;
            txtPlotPoints.Background = Brushes.DimGray;
            txtStoryEvents.Background = Brushes.DimGray;

            //Add some margin between the TextBoxes
            txtStoryType.Margin = new System.Windows.Thickness(0,0,5,0);
            txtStoryIdea.Margin = new System.Windows.Thickness(0, 10, 5, 10);
            txtPlotTwists.Margin = new System.Windows.Thickness(0, 10, 5, 10);
            txtPlotPoints.Margin = new System.Windows.Thickness(0, 10, 5, 10);
            txtStoryEvents.Margin = new System.Windows.Thickness(0, 10, 5, 10);
        }

        /// <summary>
        /// Sets the postion of all controls at the grid
        /// </summary>
        void SetControlsPostions()
        {
            //Set all the text blocks to the First Column
            Grid.SetColumn(lbStoryType, 0);
            Grid.SetColumn(lbStoryIdea, 0);
            Grid.SetColumn(lbPlotTwists, 0);
            Grid.SetColumn(lbPlotPoints, 0);
            Grid.SetColumn(lbStoryEvents, 0);
            //Set each text block to it's corrosponding row
            Grid.SetRow(lbStoryType, 0);
            Grid.SetRow(lbStoryIdea, 1);
            Grid.SetRow(lbPlotTwists, 2);
            Grid.SetRow(lbPlotPoints, 3);
            Grid.SetRow(lbStoryEvents, 4);

            //Set all the textboxes to the Second Column
            Grid.SetColumn(txtStoryType, 1);
            Grid.SetColumn(txtStoryIdea, 1);
            Grid.SetColumn(txtPlotTwists, 1);
            Grid.SetColumn(txtPlotPoints, 1);
            Grid.SetColumn(txtStoryEvents, 1);
            //Set each textbox to it's corrosponding row
            Grid.SetRow(txtStoryType, 0);
            Grid.SetRow(txtStoryIdea, 1);
            Grid.SetRow(txtPlotTwists, 2);
            Grid.SetRow(txtPlotPoints, 3);
            Grid.SetRow(txtStoryEvents, 4);
        }

        /// <summary>
        /// Adds all the controls to the grid
        /// </summary>
        void AddControlsToGrid()
        {
            //Add the TextBlocks to the grid
            Container.Children.Add(lbStoryType);
            Container.Children.Add(lbStoryIdea);
            Container.Children.Add(lbPlotTwists);
            Container.Children.Add(lbPlotPoints);
            Container.Children.Add(lbStoryEvents);

            //Add the textboxes to the grid
            Container.Children.Add(txtStoryType);
            Container.Children.Add(txtStoryIdea);
            Container.Children.Add(txtPlotTwists);
            Container.Children.Add(txtPlotPoints);
            Container.Children.Add(txtStoryEvents);
        }
        #endregion

        #region Methods
        void LoadTextBoxesText()
        {
            string combinedText = "";
            foreach(string s in storyConcepts.StoryTypes)
            {
                combinedText += s + ",";
            }
            txtStoryType.Text = combinedText;
            txtStoryIdea.Text = storyConcepts.StoryIdea;
            txtPlotTwists.Text = storyConcepts.PlotTwists;
            txtPlotPoints.Text = storyConcepts.PlotPoints;
            txtStoryEvents.Text = storyConcepts.StoryEvents;
        }

        public override void SaveFile()
        {
            //Get the current path of the file,(was saved before in the tab tag)
            string CurrentPath = this.Tag.ToString();
            //Create a StoryConcepts Class Based on the new data in the Tab
            StoryConcepts sc = new StoryConcepts()
            {
                fileName = ((TextBlock)tabHeader.Children[0]).Text,
                StoryTypes = txtStoryType.Text.Split(','),
                StoryIdea = txtStoryIdea.Text,
                PlotTwists = txtPlotTwists.Text,
                PlotPoints = txtPlotPoints.Text,
                StoryEvents = txtStoryEvents.Text
            };
            //Mark IsSaved as true
            IsSaved = true;
            //Save the file With the new data
            saveLoadSystemViewModel.Save(CurrentPath, sc);
            //Remove the unsaved star from the header
            ((TextBlock)tabHeader.Children[0]).Text = ((TextBlock)tabHeader.Children[0]).Text.Remove(((TextBlock)tabHeader.Children[0]).Text.Length - 1);
        }

        #endregion

        #region Events

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check if the file is saved
            //If it is, mark it as unsaved and add the unsaved star to the header
            if (IsSaved == true)
            {
                ((TextBlock)tabHeader.Children[0]).Text += "*";
                IsSaved = false;
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //check what the user had pressed
            //If he Pressed Ctrl+S, save the file
            if (System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control && e.Key == System.Windows.Input.Key.S)
            {
                //Check if the file is already saved
                //If not,Save it
                if (IsSaved == false)
                {
                    SaveFile();
                }
            }
        }

        #endregion

    }
}
