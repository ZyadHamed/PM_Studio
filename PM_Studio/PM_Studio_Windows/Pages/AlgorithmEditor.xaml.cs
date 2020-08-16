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

        public AlgorithmEditor()
        {
            InitializeComponent();
            
            lstFiles.ItemsSource = fileMangerViewModel.FilesAndFolders;
            txtFilePath.Text = fileMangerViewModel.filePath;
            
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
            //If the Item was a file, then do something
            if(fileMangerViewModel.IsSelectedFile == true)
            {
                
               

            }
            //If it was not a file, then it's a folder, then load it's contents
            else
            {
                lstFiles.ItemsSource = null;
                lstFiles.ItemsSource = fileMangerViewModel.NextFilesAndFolders;
                txtFilePath.Text = fileMangerViewModel.filePath;
            }
           
        }

        private void lstFiles_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Back:
                    lstFiles.ItemsSource = null;
                    lstFiles.ItemsSource = fileMangerViewModel.PreviousFilesAndFolders;
                    txtFilePath.Text = fileMangerViewModel.filePath;
                    break;

                case Key.Enter:
                    lstFiles.ItemsSource = null;
                    lstFiles.ItemsSource = fileMangerViewModel.NextFilesAndFolders;
                    txtFilePath.Text = fileMangerViewModel.filePath;
                    break;

            }
        }
    }
}
