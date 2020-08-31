using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PM_Studio
{
    public class FileManger
    {
        #region Variables
        public bool IsFile = false;
        public string selecteditem = "";
        public string filePath = @"E:\zyadhamedashour";
        public string CurrentFileText = "";
        public bool IsCut = false;
        public string CurrentCutCopyFilePath = "";
        #endregion

        #region Constructor
        public FileManger(string SelectedItem, string filePath = @"E:\zyadhamedashour")
        {
            //Set the current selected item string to the incoming selected item
            selecteditem = SelectedItem;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns all Files and Folders inside the current file Path
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<(string ItemType, string ItemName)> LoadFilesAndDirectories()
        {
            DirectoryInfo FileList;
            string tempFilePath = "";

            try
            {
                //If the currnnt item is a file, Check it's type and do something according to it
                if (IsFile == true)
                {
                    //set the current path to be the selected file path
                    tempFilePath = filePath + @"\" + selecteditem;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    //If the current file is a txt, return the text inside it
                    if (fileDetails.Extension == ".txt")
                    {
                        //StreamReader sr = new StreamReader(tempFilePath);
                        //string alltext = sr.ReadToEnd();
                        //sr.Dispose();
                        //CurrentFileText = alltext;
                    }
                    else
                    {
                        //MessageBox.Show("This Is a nice file");
                    }
                    return GoBack();
                }
                else
                {
                    //Get the files and folders in the current directory
                    FileList = new DirectoryInfo(filePath);
                    FileInfo[] files = FileList.GetFiles();
                    DirectoryInfo[] folders = FileList.GetDirectories();
                    //Make a list of the ImprovedListItem to save files data inside it
                    ObservableCollection<(string, string)> FilesAndFolders = new ObservableCollection<(string, string)>();
                    //Clear all content of list on each time to avoid duplicating files and folders
                    FilesAndFolders.Clear();

                    //Loop inside each file and folder and assign the file data into it
                    for (int i = 0; i < files.Length; i++)
                    {

                        FilesAndFolders.Add(("File", files[i].Name));

                    }

                    for (int i = 0; i < folders.Length; i++)
                    {

                        FilesAndFolders.Add(("Folder", folders[i].Name));

                    }

                    return FilesAndFolders;


                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                 return GoBack();
            }
        }


        public async Task<List<(string ItemType, string ItemName)>> LoadFilesAndDirectoriesAsync(string filepath)
        {
            DirectoryInfo FileList;
            string tempFilePath = "";

            try
            {
                //If the currnnt item is a file, Check it's type and do something according to it
                if (IsFile == true)
                {
                    //set the current path to be the selected file path
                    tempFilePath = filepath + @"\" + selecteditem;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    //If the current file is a txt, return the text inside it
                    if (fileDetails.Extension == ".txt")
                    {
                        //StreamReader sr = new StreamReader(tempFilePath);
                        //string alltext = sr.ReadToEnd();
                        //sr.Dispose();
                        //CurrentFileText = alltext;
                    }
                    else
                    {
                        //MessageBox.Show("This Is a nice file");
                    }
                    return null;
                }
                else
                {
                    //Get the files and folders in the current directory
                    FileList = new DirectoryInfo(filepath);
                    FileInfo[] files = FileList.GetFiles();
                    DirectoryInfo[] folders = FileList.GetDirectories();
                    //Make a list of the ImprovedListItem to save files data inside it
                    List<(string, string)> FilesAndFolders = new List<(string, string)>();
                    //Clear all content of list on each time to avoid duplicating files and folders
                    FilesAndFolders.Clear();

                    //Loop inside each file and folder and assign the file data into it
                    for (int i = 0; i < files.Length; i++)
                    {

                        await Task.Run(() => FilesAndFolders.Add(("File", files[i].Name)));

                    }

                    for (int i = 0; i < folders.Length; i++)
                    {

                        await Task.Run(() => FilesAndFolders.Add(("Folder", folders[i].Name)));

                    }

                    return FilesAndFolders;


                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //return await GoBack();
                return null;
            }
        }

        /// <summary>
        /// This method goes into the content of a file or a folder
        /// if it's a file,then load the content inside it
        /// if it's a folder, return the files and folders inside it as a list
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<(string ItemType, string ItemName)> GoForward()
        {
            if (IsFile == false)
            {
                //remove the backslash from the path
                RemoveBackSlash();

                //set the current path to the Selected Item Path
                filePath = filePath + @"\" + selecteditem;

                IsFile = false;
                //return all the files and folders in that path
                return LoadFilesAndDirectories();
            }
            else
            {
                //If it was not a file, go back to the path where the file was
                return GoBack();
            }
        }

        public ObservableCollection<(string ItemType, string ItemName)> GoBack()
        {
            RemoveBackSlash();

            //check if the path is not a path of a Drive or a parition

            if (filePath != "" && filePath != null && filePath.Length > 3)
            {
                //Remove the last folder from the path
                filePath = filePath.Substring(0, filePath.LastIndexOf(@"\"));
                //set the IsFile to false
                IsFile = false;
                //Load the files and folders inside that previous folder
                RemoveBackSlash();
                return LoadFilesAndDirectories();
            }
            //return the files and folders in both cases, if the back succeed, the files will change and refreash
            //else, the files will remain the same to avoid errors
            return LoadFilesAndDirectories();
        }


        /// <summary>
        /// Remove the last backslash inside a given path string
        /// </summary>
        public void RemoveBackSlash()
        {
            string path = filePath;
            if (path.LastIndexOf(@"\") == path.Length - 1)
            {
                filePath = path.Substring(0, path.Length - 1);
            }
        }

        /// <summary>
        /// Checks wheather the Current FilePath is a File or not
        /// </summary>
        /// <param name="FilePath">The File Path to check in</param>
        /// <returns></returns>

        public bool IsPathFile(string FilePath)
        {
            FileAttributes fa = File.GetAttributes(FilePath);
            if (fa == FileAttributes.Directory)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets the Extension of the Current File
        /// </summary>
        /// <param name="FilePath">The Path of the file</param>
        /// <returns></returns>

        public string GetFileExtension(string FilePath)
        {
            FileInfo fi = new FileInfo(FilePath);
            return fi.Extension;
        }

        /// <summary>
        /// Searchs for all files with a given Extension in a given filePath
        /// </summary>
        /// <param name="filePath">The Path to Search in</param>
        /// <param name="Extension">The Extension to Search with</param>
        /// <returns></returns>

        public static List<string> GetAllFilesByExtension(string filePath, string Extension)
        {
            return Directory.GetFiles(filePath, "*" + Extension).ToList();
        }

        #endregion

    }
}
