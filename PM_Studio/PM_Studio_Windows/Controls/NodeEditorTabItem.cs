using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    class NodeEditorTabItem : FileTabItem
    {
        NodesEditorCanvas Canvas = new NodesEditorCanvas();

        #region Constructor

        public NodeEditorTabItem(TabControl tabControl, string header, string filePath, NodesEditorCanvas _Canvas) : base(tabControl, header, filePath)
        {
            this.KeyDown += NodeEditorTabItem_KeyDown;
            //Define a new grid to be the container of the Items
            Grid Container = new Grid();

            //Add the Nodes Canvas to that grid
            Container.Children.Add(Canvas);

            //Add the Grid to the TabItem
            this.AddChild(Container);

            this.Focus();
        }

        private void NodeEditorTabItem_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                Canvas.DeleteSelectedNodeBlocks();
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
        }
        #endregion

    }
}
