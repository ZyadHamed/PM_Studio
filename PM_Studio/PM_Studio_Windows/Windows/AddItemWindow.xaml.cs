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
                    case "lstItemAlgorithm":
                        SelectedItem = "Algorithm";
                        break;

                    case "lstItemIdea":
                        SelectedItem = "Idea";
                        break;

                    case "lstItemNote":
                        SelectedItem = "Note";
                        break;

                    case "lstItemStoryPlanning":
                        SelectedItem = "StoryPlanning";
                        break;

                    case "lstItemCharacterPlanning":
                        SelectedItem = "CharacterPlanning";
                        break;

                    case "lstItemNodeSystem":
                        SelectedItem = "NodeSystem";
                        break;
                }
            }
            
        }

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
            this.DialogResult = true;
        }
    }
}
