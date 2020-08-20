using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PM_Studio
{
    class NodeBlock : TextBlock
    {

        #region Variables
        private Point mouseDownLocation;
        #endregion

        #region Constructor
        public NodeBlock(string NodeText, Canvas ParentCanvas)
        {
            this.Foreground = Brushes.WhiteSmoke;
            this.Background = Brushes.Orange;
            this.FontSize = 15;
            this.Text = NodeText;
            this.MouseDown += Node_MouseDown;
            this.MouseMove += Node_MouseMove;
            this.MouseUp += Node_MouseUp;
        }
 
        #endregion

        #region Events
        private void Node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                mouseDownLocation = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        private void Node_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft((TextBlock)sender, (e.GetPosition(this).X + Canvas.GetLeft((TextBlock)sender)) - mouseDownLocation.X);
                Canvas.SetTop((TextBlock)sender, (e.GetPosition(this).Y + Canvas.GetTop((TextBlock)sender)) - mouseDownLocation.Y);
            }
        }

        private void Node_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        #endregion

    }
}
