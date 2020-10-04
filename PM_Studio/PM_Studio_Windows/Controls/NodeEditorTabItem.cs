using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    class NodeEditorTabItem : FileTabItem
    {
        NodesEditorCanvas Canvas;

        #region Constructor

        public NodeEditorTabItem(TabControl tabControl, string filePath, NodeSystem _nodeSystem) : base(tabControl, _nodeSystem.fileName, filePath)
        {
            this.KeyDown += NodeEditorTabItem_KeyDown;
            //Define a new grid to be the container of the Items
            Grid Container = new Grid();

            Canvas = new NodesEditorCanvas(_nodeSystem.Nodes);

            //Add the Nodes Canvas to that grid
            Container.Children.Add(Canvas);

            //Add the Grid to the TabItem
            this.AddChild(Container);

            this.Focus();

            SaveFile();
        }

        #endregion

        #region Methods

        public override void SaveFile()
        {
            //Get the current path of the file,(was saved before in the tab tag)
            string CurrentPath = this.Tag.ToString();

            NodeSystem nodeSystem = new NodeSystem
            {
                fileName = HeaderText,
                Nodes = Canvas.GetNodes()
            };
            saveLoadSystemViewModel.Save(CurrentPath, nodeSystem);

            IsSaved = true;
        }

        #endregion

        #region Events

        private void NodeEditorTabItem_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                Canvas.DeleteSelectedNodeBlocks();
                IsSaved = false;
            }

            else if (e.Key == System.Windows.Input.Key.G)
            {
                //If the User has Pressed G, Toggle the Grid Visibilty on and Off
                //If it was Visible, Set It's Visibilty to false
                if (Canvas.IsGridVisible == true)
                {

                    Canvas.IsGridVisible = false;

                }
                //and Vise Versa
                else
                {

                    Canvas.IsGridVisible = true;

                }
            }

            //If the User has Pressed Ctrl + S, Save the File
            else if (System.Windows.Input.Keyboard.Modifiers == System.Windows.Input.ModifierKeys.Control && e.Key == System.Windows.Input.Key.S)
            {
                //If the File Was not Saved, then Save It
                if(IsSaved == false)
                {
                    SaveFile();
                }
                    
            }
        }

        #endregion

    }
}
