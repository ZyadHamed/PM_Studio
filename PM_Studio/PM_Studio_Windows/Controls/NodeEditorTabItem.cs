using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    class NodeEditorTabItem : FileTabItem
    {

        #region Constructor
        public NodeEditorTabItem(TabControl tabControl, string header, string filePath, NodesEditorCanvas Canvas) : base(tabControl, header, filePath)
        {
            //Define a new grid to be the container of the Items
            Grid Container = new Grid();
            //Add the Nodes Canvas to that grid
            Container.Children.Add(Canvas);
            //Add the Grid to the TabItem
            this.AddChild(Container);
        }
        #endregion

    }
}
