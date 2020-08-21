using System;
using System.Collections.Generic;
using System.Text;
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
        #endregion

        #region Constuctor
        public StoryConceptsTabItem(TabControl tabControl, string header, string filePath) : base(tabControl, header, filePath)
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

            SetTextBlocksProperties();
            SetTextBoxesProperties();
            AddControlsToGrid();
            SetControlsPostions();
            this.AddChild(Container);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Sets the text and the ForeColor for all the textblocks in the class
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

    }
}
