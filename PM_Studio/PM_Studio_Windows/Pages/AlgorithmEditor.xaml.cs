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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for AlgorithmEditor.xaml
    /// </summary>
    public partial class AlgorithmEditor : Page
    {
        FileMangerViewModel fileMangerViewModel = new FileMangerViewModel(@"E:\zyadhamedashour");
        SaveLoadSystemViewModel saveLoadSystemViewModel;
        public AlgorithmEditor()
        {
            InitializeComponent();
            
            lstFiles.ItemsSource = fileMangerViewModel.FilesAndFolders;
            txtFilePath.Text = fileMangerViewModel.filePath;
            saveLoadSystemViewModel = new SaveLoadSystemViewModel(tbFiles);
        }

        #region Events
        private void lstFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFiles.Items.Count > 0 && lstFiles.Items != null && lstFiles.SelectedItems.Count > 0)
            {
                string TempSelectedItem = ((ImagelistItem)e.AddedItems[0]).itemText;
                fileMangerViewModel.SelectedItem = TempSelectedItem;
                txtFilePath.Text = fileMangerViewModel.filePath + @"\" + TempSelectedItem;
            }

        }

        private void lstFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //If the Item was a file, Check it's extension and open it
            if(fileMangerViewModel.IsSelectedFile == true)
            {
                //If it was an algorithm file, open it in an Algorithm Tab Item
                if(fileMangerViewModel.getSelectedExtension == ".algorithm")
                {
                    //Create an algorithm tabItem using the current selected Algorithm file
                    AlgorithmTabItem algorithmTabItem = saveLoadSystemViewModel.ReturnAlgorithm(fileMangerViewModel.filePath + @"\" + fileMangerViewModel.SelectedItem);
                    //Add that tab Item to the tab control
                    tbFiles.Items.Add(algorithmTabItem);
                }

                //If it was a story file, open it in a StoryConcepts Tab Item
                else if(fileMangerViewModel.getSelectedExtension == ".story")
                {
                    //Create a storyConcepts TabItem using the current selected Story File
                    StoryConceptsTabItem storyConceptsTabItem = saveLoadSystemViewModel.ReturnStoryConceptsTabItem(fileMangerViewModel.filePath + @"\" + fileMangerViewModel.SelectedItem);
                    //Add that tab Item to the tab control
                    tbFiles.Items.Add(storyConceptsTabItem);
                }

            }
            //If it was not a file, then it's a folder, then load it's contents
            else
            {
                //Set the Items Source for the Files Listview to null
                lstFiles.ItemsSource = null;
                //Reset the Items Source to the new List of files and folders
                lstFiles.ItemsSource = fileMangerViewModel.NextFilesAndFolders;
                //Update the file path textbox to match the new path
                txtFilePath.Text = fileMangerViewModel.filePath;
            }
           
        }

        private void lstFiles_KeyDown(object sender, KeyEventArgs e)
        {
            //Check what was the pressed key
            switch (e.Key)
            {
                //If the key was BackSpace, then load the previous files and folders
                case Key.Back:
                    lstFiles.ItemsSource = null;
                    lstFiles.ItemsSource = fileMangerViewModel.PreviousFilesAndFolders;
                    txtFilePath.Text = fileMangerViewModel.filePath;
                    break;
                
                //If the key was Enter, then load the files and folders inside the selected folder
                case Key.Enter:
                    lstFiles.ItemsSource = null;
                    lstFiles.ItemsSource = fileMangerViewModel.NextFilesAndFolders;
                    txtFilePath.Text = fileMangerViewModel.filePath;
                    break;

            }
        }

        private void menuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            //Show the AddItemWindow as a Dialog
            AddItemWindow addItemWindow = new AddItemWindow();
            //If the User Clicked Ok in the Window, then Add the Selected Item
            if (addItemWindow.ShowDialog() == true)
            {
                //Swich on the Selected Item Inside the AddItem Window
                switch (addItemWindow.SelectedItem)
                {
                    //If the Selected Item was Algorithm, then Create a Blank Algorithm File
                    case "Algorithm":
                        //Create a Blank Algorithm File in the Selected filePath
                        saveLoadSystemViewModel.CreateAlgorithmFile(txtFilePath.Text + @"\", addItemWindow.ItemName);
                        //Reload the File Explorer
                        lstFiles.ItemsSource = null;
                        lstFiles.ItemsSource = fileMangerViewModel.FilesAndFolders;
                        break;

                    //If the Selected Item was Idea, then Create a Blank Idea File(Code will be added soon)
                    case "Idea":

                        break;

                    //If the Selected Item was Note, then Create a Blank Idea File(Code will be added soon)
                    case "Note":

                        break;

                    //If the Selected Item was Story Planning, then Create a Blank Idea File(Code will be added soon)
                    case "StoryPlanning":

                        break;

                    //If the Selected Item was Character Planning, then Create a Blank Idea File(Code will be added soon)
                    case "CharacterPlanning":

                        break;

                    //If the Selected Item was Node System, then Create a Blank Node System File(Code will be added soon)
                    case "NodeSystem":

                        break;
                }
               
            }
        }

        private void lstFiles_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Check If there is a Selected Item to avoid removing Extra text from the FilePath
            //If there is a Selected Item, then remove it's Name from the File Path and Unselect All Items
            if(lstFiles.SelectedItems.Count > 0)
            {
                //Get the postion of the place of the click
                HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
                //Check the type of Item user pressed
                //If the user didn't click a List Item, then he must have clicked a black space
                if (r.VisualHit.GetType() != typeof(ListBoxItem))
                {
                    //Unselect all Items
                    lstFiles.UnselectAll();
                    //Remove the Prevoisly selected Item from the Text of the TextBox
                    txtFilePath.Text = txtFilePath.Text.Remove(txtFilePath.Text.LastIndexOf(@"\"));
                }

            }
        }

        #endregion
    }
}
