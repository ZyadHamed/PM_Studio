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
        private List<Line> fromLines = new List<Line>();
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
        //           \ <----(Block1 ToLine, Block2 FromLine1)
        //            \
        //             \
        //              \
        //               \                                            __________
        //         (x2,y2)\ __________                               |          |
        //                 |          |                             /|  Block4  |
        //                 |  Block2  |                            / |__________|
        //                 |__________|                           /
        //                      (x1,y1)\                         /
        //                              \                       /
        //         (Block2 ToLine,       \                     /
        //        Block3 FromLine1) ----> \                   / <----(Block4 ToLine, Block3 FromLine2)
        //                                 \                 /
        //                                  \               /
        //                                   \             /
        //                             (x2,y2)\ __________/
        //                                     |          |
        //                                     |  Block3  |
        //                                     |__________|
        //
        // From Line Is the Line Entering the Node Block        
        // To Line Is the Line Exiting from the Node Block 
        public void SetLinesPostion()
        {
            //If the Node Has FromLines and a ToLine, Move all Lines with repect to the coordinates of the Block itself
            if (FromLines.Count > 0 && ToLine != null)
            {
                
                //Loop inside all the Lines inside the FromLines list
                foreach(Line line in FromLines)
                {
                    // Set the (x2,y2) Coordinates of that FromLine(The End of the FromLine) to the coordinates of the Block
                    line.X2 = Canvas.GetLeft(this);
                    line.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;
                }

                // Set the (x1,y1) Coordinates of the ToLine(The Start of the ToLine) to the coordinates of the Block
                ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth;
                ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
            }

            //else If the Node has only a ToLine, Move it with respect to the coordinates of the block
            else if (FromLines.Count <= 0 && ToLine != null)
            {
                // Set the (x1,y1) Coordinates of the ToLine(The Start of the ToLine) to the coordinates of the Block
                ToLine.X1 = Canvas.GetLeft(this) + this.ActualWidth;
                ToLine.Y1 = Canvas.GetTop(this) + this.ActualHeight / 2;
            }

            //else if the Node has only FromLines, Move them with respect to the coordinates of the block
            else if (FromLines.Count > 0 && ToLine == null)
            {
                //Loop inside all the Lines inside the FromLines list
                foreach (Line line in FromLines)
                {
                    // Set the (x2,y2) Coordinates of that FromLine(The End of the FromLine) to the coordinates of the Block
                    line.X2 = Canvas.GetLeft(this);
                    line.Y2 = Canvas.GetTop(this) + this.ActualHeight / 2;
                }
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

        /// <summary>
        /// The Lines that Enter the NodeBlock and Connects it with the previous blocks
        /// </summary>
        public List<Line> FromLines
        {
            get
            {
                return fromLines;
            }
            set
            {
                //On setting the Line, set the postion of the new Line to it's new Postion
                fromLines = value;
                SetLinesPostion();
            }
        }

        #endregion

    }
}
