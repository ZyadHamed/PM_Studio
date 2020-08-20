using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PM_Studio
{
    class NodesEditorTabItem : Canvas // : FileTabItem
    {

        Node node;
        
        public NodesEditorTabItem(TabControl tabControl, string header, string filePath) // : base(tabControl, header, filePath)
        {
         
            this.Background = Brushes.Blue;
            node = new Node("New Node",this);
            this.Children.Add(node);
            this.Children.Add(new Node("New Node2", this));
            
            Canvas.SetLeft(node, 150);
            Canvas.SetTop(node, 200);

            Canvas.SetLeft(this.Children[1], 160);
            Canvas.SetTop(this.Children[1], 190);
        }
        
        

        
    }
}
