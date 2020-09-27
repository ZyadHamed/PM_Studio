using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PM_Studio
{
    public class NodeBlock : TextBlock
    {

        #region Variables

        private Point mouseDownLocation;
        public Line FromLine;
        public Line ToLine;
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

            //line.X2 = Canvas.GetLeft(this);
            //line.Y2 = Canvas.GetTop(this);
            //line.arrow.X1 = 0;
            //line.arrow.Y1 = 0;

            
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
                if(FromLine != null && ToLine != null)
                {
                    FromLine.X2 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                    FromLine.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;
                    ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                    ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
                }
                else if(FromLine == null && ToLine != null)
                {
                    ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                    ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
                }
                else if(FromLine != null && ToLine == null)
                {
                    FromLine.X2 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                    FromLine.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;
                }
            }
        }

        private void Node_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
        }

        #endregion

    }
}
