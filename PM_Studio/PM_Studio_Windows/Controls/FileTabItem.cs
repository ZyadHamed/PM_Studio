using System.Windows;
using System.Windows.Controls;

namespace PM_Studio
{
    class FileTabItem : TabItem
    {
        #region Variables

        private bool isSaved = true;

        protected SaveLoadSystemViewModel saveLoadSystemViewModel;
        protected StackPanel tabHeader = new StackPanel();
        protected TextBlock headerText = new TextBlock();
        protected Button closeButton = new Button();
        protected TabControl tabControl;
        protected Grid Container = new Grid();
        protected StackPanel buttonsBar = new StackPanel();

        #endregion

        #region Constructor

        public FileTabItem(TabControl TabControl, string Header = "", string filePath = "")
        {
            //Add two rows for the container Grid, one for the buttonsBar, and the other for the TabItem Contents themselves
            Container.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            Container.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            //Set the Properties of the buttonsBar and add it to the contanier
            buttonsBar.Orientation = Orientation.Horizontal;
            buttonsBar.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2D2D30"));
            Container.Children.Add(buttonsBar);
            Grid.SetRow(buttonsBar, 0);

            //Set the tabControl field to the incoming TabControl
            tabControl = TabControl;

            //Set the FilePath Property to the incoming filePath
            FilePath = filePath;

            //Set the tabControl Paramerter of the SaveLoadSystemViewModel to tabControl
            saveLoadSystemViewModel = new SaveLoadSystemViewModel(tabControl);

            //Set the properties of the closing button
            closeButton.Content = "X";
            closeButton.BorderThickness = new System.Windows.Thickness(0);

            //Add the click event to the closing button
            closeButton.Click += closeButton_Click;

            //Set the header text to the incoming text, and add the closing button
            headerText.Text = Header;
            tabHeader.Orientation = Orientation.Horizontal;
            tabHeader.Children.Add(headerText);
            tabHeader.Children.Add(closeButton);

            //Set the header of the tab to the incoming header, and the tag to the file path
            this.Header = tabHeader;

            //Set the child of the tabItem to the Container
            this.AddChild(Container);
            
        }

        #endregion

        #region Methods
        public virtual void SaveFile()
        {

        }
        #endregion

        #region Events
        private void closeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Check If value of IsSaved is false
            //If yes, then the file isn't saved yet
            if (IsSaved == false)
            {
                //Ask the user wheather to save this file before closing or not
                if (MessageBox.Show("File \"" + ((TextBlock)tabHeader.Children[0]).Text + "\" Hasn't been saved yet\n Do you Want to Save it Before closing?", "Save file before close", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //If yes, save the file and remove the tab
                    SaveFile();
                    tabControl.Items.Remove(this);
                }
                //If not, Quit without saving
                else
                {
                    tabControl.Items.Remove(this);
                }

            }
            //If the file Is already saved, Close the tab
            else
            {
                tabControl.Items.Remove(this);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Text of the Header of the TabItem
        /// </summary>
        public string HeaderText
        {
            get
            {
                return headerText.Text;
            }
            set
            {
                headerText.Text = value;
            }
        }

        /// <summary>
        /// The Boolean which indicates wheather the file IsSaved or Not
        /// </summary>
        public bool IsSaved
        {
            get
            {
                return isSaved;
            }
            set
            {
                isSaved = value;
                //If the Incoming value was true, Remove the Unsaved Star from the Header
                if (isSaved == true)
                {
                    if (HeaderText[HeaderText.Length - 1] == '*')
                    {
                        HeaderText = HeaderText.Remove(HeaderText.Length - 1);
                    }

                }

                //Else, then the incoming value is false, then add an Unsaved Star to the Header
                else
                {
                    if(HeaderText[HeaderText.Length - 1] != '*')
                    {
                        HeaderText += "*";
                    }

                }
                
            }
        }

        public string FilePath { get; set; }

        #endregion

    }
}
