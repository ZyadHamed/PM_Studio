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
        SaveLoadSystemViewModel saveLoadSystemViewModel = new SaveLoadSystemViewModel();
        public AlgorithmEditor()
        {
            InitializeComponent();
            
            lstFiles.ItemsSource = fileMangerViewModel.FilesAndFolders;
            txtFilePath.Text = fileMangerViewModel.filePath;
            //StoryConcepts sc = new StoryConcepts()
            //{
            //    fileName = "Untitled Story.story",
            //    StoryTypes = "Adventure, Talend".Split(','),
            //    StoryIdea = "Hero Is born and Dead!",
            //    PlotTwists = "He is born\n Now he is dead lol",
            //    PlotPoints = "Nothing, Just make the hero die",
            //    StoryEvents = "All the above fool"
            //};
            //SaveLoadSystem.SaveData(@"E:\zyadhamedashour\" + sc.fileName, sc);
        }

        private void lstFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFiles.Items.Count > 0 && lstFiles.Items != null)
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
                    //Set the tab Control of that AlgorithmTabItem to tbFiles 
                    algorithmTabItem.tabControl = tbFiles;
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
    }
}
