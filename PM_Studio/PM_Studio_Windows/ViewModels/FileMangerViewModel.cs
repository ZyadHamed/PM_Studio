using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PM_Studio
{
    class FileMangerViewModel : INotifyPropertyChanged
    {
        #region Variables

        public string currentPath;
        public event PropertyChangedEventHandler PropertyChanged;
        List<string> FilesType = new List<string>();
        FileManger fileManger = new FileManger("");


        #endregion

        #region Constrcutor

        public FileMangerViewModel(string _currentPath)
        {
            //Set the Current FilePath to the incoming path
            currentPath = _currentPath;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns an List Containing the Files Icon and Name
        /// this List Can be used to fill the ListView then
        /// This Method Uses the FileManger FileLoading Method as a Base
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ImagelistItem> GetFilesAndFolders()
        {
            //Create an Empty List of Files Icons and Names
            ObservableCollection<ImagelistItem> returnedFilesAndFolders = new ObservableCollection<ImagelistItem>();
            //Make a List Containing the files and folders Name and Type
            var FilesAndFolders = fileManger.LoadFilesAndDirectories();
            //Loop inside each item in there
            for (int i = 0; i < FilesAndFolders.Count; i++)
            {
                //If the item type was a File, Set the icon to file icon, then add the corrosponding File Name
                if (FilesAndFolders[i].ItemType == "File")
                {
                    returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/File.png", FilesAndFolders[i].ItemName));
                }
                //If it's not, then it must be a folder, and then set the icon to folder icon, then add the corrosponding Folder Name
                else
                {
                    returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/Folder.png", FilesAndFolders[i].ItemName));
                }
            }
            //return the filled List At the end
            return returnedFilesAndFolders;
        }

        /// <summary>
        /// Loads the files and folders inside a Selected Item Path in a list of files names and icons
        /// this List Can be used to fill the ListView then
        /// this Selected item is obtained when the user selects any item
        /// This Method uses the files and folders returned from fileManger.GoForward as a base
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ObservableCollection<ImagelistItem> GetNextFilesAndFolders(string filePath)
        {
            //Check wheather the selected item is a File or not

            //If the selected item is a Folder, then we can Load It's Contents
            if (fileManger.IsFile == false)
            {
                //Create an Empty List of Files Icons and Names
                ObservableCollection<ImagelistItem> returnedFilesAndFolders = new ObservableCollection<ImagelistItem>();
                //Make a List Containing the files and folders Name and Type of the inside content of selected folder
                var FilesAndFolders = fileManger.GoForward();
                for (int i = 0; i < FilesAndFolders.Count; i++)
                {
                    //If the item type was a File, Set the icon to file icon, then add the corrosponding File Name
                    if (FilesAndFolders[i].ItemType == "File")
                    {
                        returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/File.png", FilesAndFolders[i].ItemName));
                    }
                    //If it's not, then it must be a folder, and then set the icon to folder icon, then add the corrosponding Folder Name
                    else
                    {
                        returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/Folder.png", FilesAndFolders[i].ItemName));
                    }
                }
                //return the filled List At the end
                return returnedFilesAndFolders;
            }
            //If It's not, then it's a file, then we need to take another action
            else
            {
                return null;
            }


        }

        /// <summary>
        /// Loads the files and folders inside the previous folder Path in a list of files names and icons
        /// this List Can be used to fill the ListView then
        /// This Method uses the files and folders returned from fileManger.GoBack as a base
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ObservableCollection<ImagelistItem> GetPreviousFilesAndFolders(string filePath)
        {
            //Create an Empty List of Files Icons and Names
            ObservableCollection<ImagelistItem> returnedFilesAndFolders = new ObservableCollection<ImagelistItem>();
            //Make a List Containing the files and folders Name and Type of the content of the previous folder
            var FilesAndFolders = fileManger.GoBack();
            for (int i = 0; i < FilesAndFolders.Count; i++)
            {
                //If the item type was a File, Set the icon to file icon, then add the corrosponding File Name
                if (FilesAndFolders[i].ItemType == "File")
                {
                    returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/File.png", FilesAndFolders[i].ItemName));
                }
                //If it's not, then it must be a folder, and then set the icon to folder icon, then add the corrosponding Folder Name
                else
                {
                    returnedFilesAndFolders.Add(new ImagelistItem("pack://application:,,,/PM_Studio_Windows;component/Images/Folder.png", FilesAndFolders[i].ItemName));
                }
            }
            //Set the current path to the previous folder path(which is stored in the FileManger Class)
            currentPath = fileManger.filePath;
            //return the filled List At the end
            return returnedFilesAndFolders;
        }


        #endregion

        #region Properties
        /// <summary>
        /// Gets the result from the GetFilesAndFolders Method and returns it
        /// Sets the incoming value to this property after clearing the existing items first
        /// </summary>
        public ObservableCollection<ImagelistItem> FilesAndFolders
        {
            get
            {
                return GetFilesAndFolders();
            }
            set
            {
                FilesAndFolders.Clear();
                FilesAndFolders = value;
            }
        }

        /// <summary>
        /// Gets the content of the selected folder and returns it, then updates the path to that folder path
        /// </summary>
        public ObservableCollection<ImagelistItem> NextFilesAndFolders
        {
            get
            {

                var returnedItem = GetNextFilesAndFolders(currentPath);
                currentPath = fileManger.filePath;
                return returnedItem;
            }

        }
        /// <summary>
        /// Gets the content of the preivous folder and returns it, then updates the path to that folder path
        /// </summary>
        public ObservableCollection<ImagelistItem> PreviousFilesAndFolders
        {
            get
            {
                currentPath = fileManger.filePath;
                var returnedItem = GetPreviousFilesAndFolders(currentPath);

                return returnedItem;
            }
        }

        /// <summary>
        /// Gets the current file path and returns it
        /// sets the path to the incoming path then updates all pathes to that path, then loads files and folders in that path
        /// </summary>
        public string filePath
        {
            get
            {
                return currentPath;
            }

            set
            {
                //Set the current path to the incoming path
                currentPath = value;
                //Set the file manger file path to that path
                fileManger.filePath = currentPath;
                //Raise the change event
                RaisePropertyChanged(nameof(filePath));
                //Clear the current items in the list
                FilesAndFolders.Clear();
                //Reload the list with the files and folders inside that new path
                FilesAndFolders = GetFilesAndFolders();
            }

        }

        public string SelectedItem
        {
            get
            {
                return fileManger.selecteditem;
            }
            set
            {
                if (fileManger.IsPathFile(filePath + @"\" + value) == true)
                    fileManger.IsFile = true;
                else
                    fileManger.IsFile = false;
                fileManger.selecteditem = value;
            }
        }

        public bool IsSelectedFile
        {
            get 
            {
                return fileManger.IsPathFile(filePath + @"\" + SelectedItem);
            }
        }

        public string getSelectedExtension
        {
            get
            {
                return fileManger.GetFileExtension(filePath + @"\" + SelectedItem);
            }
        }

        #endregion

        #region Events
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
