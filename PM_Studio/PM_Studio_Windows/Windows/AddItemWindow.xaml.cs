using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private string selectedItem = "";
        public AddItemWindow()
        {
            InitializeComponent();
            //lstItems.SelectedItem = lstItems.Items[0];
            
        }

        private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems != null)
            {
                switch ((lstItems.SelectedItem as StackPanel).Name)
                {
                    //If the Selected Item was an Algorithm, Set the Header Containing the Item Name to Algorithm
                    //And set the describing text to The Algorithm Description(Will be Added Soon)
                    case "lstItemAlgorithm":
                        SelectedItem = "Algorithm";
                        lbItemName.Text = "Algorithm";
                        lbItemDescription.Text = "";
                        break;

                    //If the Selected Item was an Idea, Set the Header Containing the Item Name to Idea
                    //And set the describing text to The Idea Description
                    case "lstItemIdea":
                        SelectedItem = "Idea";
                        lbItemName.Text = "Idea";
                        lbItemDescription.Text = "an Idea of something in your head\nand the subideas related to this idea";
                        break;

                    //If the Selected Item was a Note, Set the Header Containing the Item Name to Note
                    //And set the describing text to The Note Description
                    case "lstItemNote":
                        SelectedItem = "Note";
                        lbItemName.Text = "Note";
                        lbItemDescription.Text = "Some random text you want to store temporarly\nor maybe some notes to consider in your project";
                        break;

                    //If the Selected Item was a Story Planning, Set the Header Containing the Item Name to Story Planning
                    //And set the describing text to The Story Planning Description
                    case "lstItemStoryPlanning":
                        SelectedItem = "StoryPlanning";
                        lbItemName.Text = "Story Planning";
                        lbItemDescription.Text = "The Main Concepts of your story\nlike what does it talk about?, how the plot changes over time?\nwhat are the main events in the story?, etc";
                        break;

                    //If the Selected Item was a Character Planning, Set the Header Containing the Item Name to Character Planning
                    //And set the describing text to The Character Planning Description(Will be Added Soon)
                    case "lstItemCharacterPlanning":
                        SelectedItem = "CharacterPlanning";
                        lbItemName.Text = "Character Planning";
                        break;

                    //If the Selected Item was a Node System, Set the Header Containing the Item Name to Node System
                    //And set the describing text to The Node System Description
                    case "lstItemNodeSystem":
                        SelectedItem = "NodeSystem";
                        lbItemName.Text = "Node System";
                        lbItemDescription.Text = "The Visualization of Some logical steps\nlike an Algorthim, the way something works, etc";
                        break;
                }
            }
            
        }

        /// <summary>
        /// The Selected Item To Add
        /// </summary>
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
            }
        }

        /// <summary>
        /// The Name of the Item To Add
        /// </summary>
        public string ItemName
        {
            get
            {
                return txtItemName.Text;
            }
            set
            {
                txtItemName.Text = value;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Set the Dialog Result of the Form to Ok
            this.DialogResult = true;
        }
    }
}
