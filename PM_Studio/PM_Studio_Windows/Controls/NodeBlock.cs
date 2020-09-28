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
        private Line fromLine;
        private Line toLine;
        #endregion

        #region Constructor
        public NodeBlock(string NodeText)
        {
            this.Foreground = Brushes.WhiteSmoke;
            this.Background = Brushes.Orange;
            this.FontSize = 15;
            this.Text = NodeText;
            this.MouseDown += Node_MouseDown;
            this.MouseMove += Node_MouseMove;
            this.MouseUp += Node_MouseUp;
            SetLinesPostion();
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the Posion of Lines with respect to the postion of the NodeBlock
        /// </summary>

        // __________
        //|          |
        //|  Block1  |
        //|__________|
        // (x1,y1)\
        //         \
        //          \
        //           \           (Block2 FromLine)
        //            \
        //             \
        //              \
        //               \
        //         (x2,y2)\ __________
        //                 |          |
        //                 |  Block2  |
        //                 |__________|
        //                      (x1,y1)\
        //                              \
        //                               \
        //                                \        (Block2 ToLine)
        //                                 \
        //                                  \
        //                                   \
        //                             (x2,y2)\ __________
        //                                     |          |
        //                                     |  Block3  |
        //                                     |__________|
        //
        // From Line Is the Line Entering the Node Block        
        // To Line Is the Line Exiting from the Node Block 
        void SetLinesPostion()
        {
            //If the Node Has a FromLine and a ToLine, Move both Lines with repect to the coordinates of the Block itself
            if (FromLine != null && ToLine != null)
            {
                // Set the (x2,y2) Coordinates of the FromLine(The End of the FromLine) to the coordinates of the Block
                FromLine.X2 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                FromLine.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;

                // Set the (x1,y1) Coordinates of the ToLine(The Start of the ToLine) to the coordinates of the Block
                ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
            }

            //else If the Node has only a ToLine, Move it with respect to the coordinates of the block
            else if (FromLine == null && ToLine != null)
            {
                // Set the (x1,y1) Coordinates of the ToLine(The Start of the ToLine) to the coordinates of the Block
                ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
            }

            //else if the Node has only a FromLine, Move it with respect to the coordinates of the block
            else if (FromLine != null && ToLine == null)
            {
                // Set the (x2,y2) Coordinates of the FromLine(The End of the FromLine) to the coordinates of the Block
                FromLine.X2 = Canvas.GetLeft(this) + this.ActualWidth / 2;
                FromLine.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;
            }
        }

        #endregion

        #region Events
        private void Node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //On Pressing the Left Mouse Button, set the MouseDownLocation Variable to the MouseLocation and Start Capturing the Mouse
            if (e.ChangedButton == MouseButton.Left)
            {
                mouseDownLocation = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        private void Node_MouseMove(object sender, MouseEventArgs e)
        {
            //If the Mouse was moved while the Left Mouse button is pressed, Move the NodeBlock
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Set the Canvas Left and the Top Properties of the NodeBlock to the Postion of the Mouse Cursor
                //(Postion of Mouse cursor is obtained by adding the Coordinates of the NodeBlock and the Current Coordinates of the Cursor and subtract from them the mouseDownLocation to remove the offset)
                Canvas.SetLeft((TextBlock)sender, (e.GetPosition(this).X + Canvas.GetLeft((TextBlock)sender)) - mouseDownLocation.X);
                Canvas.SetTop((TextBlock)sender, (e.GetPosition(this).Y + Canvas.GetTop((TextBlock)sender)) - mouseDownLocation.Y);

                //Reset the Position of the Lines of the control
                SetLinesPostion();

            }
        }

        private void Node_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //If the Left Mouse button was released, UnCapture the NodeBlock
            Mouse.Capture(null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Line that Enters the NodeBlock and Connects it with the previous block
        /// </summary>
        public Line FromLine
        {
            get
            {
                return fromLine;
            }
            set
            {
                //On setting the Line, set the postion of the new Line to it's new Postion
                fromLine = value;
                SetLinesPostion();
            }
        }

        /// <summary>
        /// The Line that Exits from the NodeBlock and Connects it with the next block
        /// </summary>
        public Line ToLine
        {
            get
            {
                return toLine;
            }
            set
            {
                //On setting the Line, set the postion of the new Line to it's new Postion
                toLine = value;
                SetLinesPostion();
            }
        }

        #endregion

    }
}
