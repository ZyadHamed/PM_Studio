﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PM_Studio
{
    public class NodesEditorCanvas : Canvas
    {
        #region Variables

        List<string> blocks = new List<string>() { "Block1", "Block2", "Block3", "Block4", "Block5", "Block6" };
        List<NodeBlock> SelectedBlocks = new List<NodeBlock>();

        private bool isGridVisible = false;
        #endregion

        #region Constructor

        public NodesEditorCanvas()
        {
            this.MouseDown += NodesEditorCanvas_MouseDown;

            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E292C"));

            FillCanvas(blocks);

        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects 2 Blocks with Each Other
        /// </summary>
        /// <param name="FirstBlock">The First Block</param>
        /// <param name="SecondBlock">The Second Block</param>
        void Connect(NodeBlock FirstBlock, NodeBlock SecondBlock)
        {
            //Create the Arrow that will connect both blocks and set it's color to Ghost white and add some Stroke thickness
            Arrow arrow = new Arrow();
            arrow.Stroke = Brushes.GhostWhite;
            arrow.StrokeThickness = 3;

            //Add that Arrow to the Canvas
            this.Children.Add(arrow);

            //Set the ToArrow of the First Block to that Arrow
            FirstBlock.ToArrow = arrow;

            //Add that Arrow to the List of the FromLines of the Second Block(such that the Arrow Exits FirstBlock and Enters Second Block)
            SecondBlock.FromArrows.Add(arrow);

            //Reset the Arrows Position so that they match the coordinates of the blocks
            FirstBlock.SetArrowsPostion();
            SecondBlock.SetArrowsPostion();
        }


        /// <summary>
        /// Creates and Addes a Number of NodeBlocks based on a List that represents their text
        /// </summary>
        /// <param name="blocksText">The List that Contains all the Blocks text</param>
        void FillCanvas(List<string> blocksText)
        {
            List<NodeBlock> blocks = new List<NodeBlock>();
            //Loop inside each string in the string List
            for (int i = 0; i < blocksText.Count; i++)
            {
                //Create a NodeBlock Based on that string
                NodeBlock block = new NodeBlock(blocksText[i]);

                //Add that NodeBlock to the List and add it to the Canvas
                blocks.Add(block);
                this.Children.Add(block);

                //Create a Random Variable that Ranges between 2 and 800 for creating a random width and between 2 and 600 for a random height
                System.Random random = new System.Random();
                int X1 = random.Next(2, 800);
                int Y1 = random.Next(2, 600);

                //Set the Coordinates of the block using the value from that random variable
                Canvas.SetLeft(block, X1);
                Canvas.SetTop(block, Y1);
            }

            //Now loop inside each NodeBlock in the NodeBlock list
            for (int i = 0; i < blocks.Count; i++)
            {
                //If we have not reached the Last block yet(when i + 1 = the Number of items this means we have reached the Last Element)
                //Connect that block with the next block
                if (i + 1 < blocks.Count)
                {
                    Connect(blocks[i], blocks[i + 1]);
                }
            }
        }

        /// <summary>
        /// Unselects all NodeBlocks in the Canvas
        /// </summary>
        void UnSelectAllNodeBlocks()
        {
            //Loop inside each Item in the Canvas
            foreach (var block in this.Children)
            {
                //If the Type of the Item Was a NodeBlock, UnSelect It
                if (block.GetType() == typeof(NodeBlock))
                {
                    NodeBlock nodeBlock = block as NodeBlock;
                    nodeBlock.IsSelected = false;
                }
            }
            //Clear the List of SelectedBlocks
            SelectedBlocks.Clear();
        }

        public void DeleteSelectedNodeBlocks()
        {
            //Create a List of Arrows to Contain the Arrows that Shall be Removed
            List<Arrow> arrowsToRemove = new List<Arrow>();

            //Loop on each block in the SelectedBlocks List and Break It's RelationShip with the other Blocks
            foreach (NodeBlock SelectedBlock in SelectedBlocks)
            {
                //Now Loop inside all the NodeBlocks in the Canvas
                foreach (var Control in this.Children)
                {
                    if (Control.GetType() == typeof(NodeBlock))
                    {
                        //Create a NodeBlock that stores the Current NodeBlock in the Loop
                        NodeBlock block = Control as NodeBlock;

                        //Check if the FromArrows List in that block contains the ToArrow of the Current SelectedBlock in the Loop
                        Arrow blockFromArrow = block.FromArrows.Find(x => x == SelectedBlock.ToArrow);

                        //If the arrow was not null, then there was an arrow going from that Selected Block into the Current Block, then Remove that arrow and break the relationship
                        if (blockFromArrow != null)
                        {
                            //Remove that arrow from the FromArrows of that Block
                            block.FromArrows.Remove(blockFromArrow);

                            //Add that arrow to the arrowsToRemove List
                            arrowsToRemove.Add(blockFromArrow);
                        }
                        //Now Check if the ToArrow of the Current Block in the Loop Is a From arrow in the Current SelectedBlock in the Loop
                        //(Check if there is a arrow that goes from the Current Block in the Loop into the Current SelectedBlock in the Loop)
                        Arrow blockToArrow = SelectedBlock.FromArrows.Find(x => x == block.ToArrow);

                        //If the arrow was not null, then the arrow that connects both blocks exist, then Remove that arrow and Break the relationship
                        if (blockToArrow != null)
                        {
                            //Remove that arrow from the FromArrows of the Current SelectedBlock
                            SelectedBlock.FromArrows.Remove(blockToArrow);

                            //Add that arrow to the arrowsToRemove List
                            arrowsToRemove.Add(blockToArrow);
                        }
                    }
                }
            }

            //After Breaking all the RelationShips between the Selected NodeBlocks and the other Blocks, Delete the Unused Lines and the Selected Blocks

            //Loop inside each arrow on the arrowsToRemove List
            foreach (Arrow arrow in arrowsToRemove)
            {
                //Delete that arrow from the Canvas
                this.Children.Remove(arrow);
            }

            //Now Loop Inside each block in the SelectedBlocks List
            foreach (NodeBlock block1 in SelectedBlocks)
            {
                //Delete that Block From the Canvas
                this.Children.Remove(block1);
            }
        }

        #endregion

        #region Events

        private void NodesEditorCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //If there was at least 1 nodeblock in the Canvas, do the Selection
            if (this.Children.Count > 0)
            {
                //If the User pressed Left Button
                if (e.ChangedButton == MouseButton.Left)
                {
                    //Create a HitTestResult for storing the object in which the user has clicked
                    HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));

                    //If the user has not clicked a NodeBlock, then He has clicked on the Canvas, then Unselect all the NodeBlocks
                    if (r.VisualHit.GetType() != typeof(NodeBlock))
                    {
                        UnSelectAllNodeBlocks();
                    }

                    //else if the User selected a NodeBlock and Was Holding Ctrl at the Same Time ,Select that Block and  Add it to the Selected Blocks List
                    else if (r.VisualHit.GetType() == typeof(NodeBlock) && Keyboard.IsKeyDown(Key.LeftCtrl) == true)
                    {
                        //Get the NodeBlock that the user has Clicked
                        NodeBlock block = r.VisualHit as NodeBlock;

                        if(SelectedBlocks.Find(x => x == block) == null)
                        {
                            //Set the Property IsSelected to true
                            block.IsSelected = true;

                            //Add this Block to the SelectedBlocks List
                            SelectedBlocks.Add(block);
                        }
                    }

                    //Else if the User Selected a NodeBlock and Was Holding Alt at the Same Time, Unselect that Block and Remove it From the Selected Blocks List
                    else if (r.VisualHit.GetType() == typeof(NodeBlock) && Keyboard.IsKeyDown(Key.LeftAlt) == true)
                    {

                        //Get the NodeBlock that the user has Clicked
                        NodeBlock block = r.VisualHit as NodeBlock;

                        //Set the Property IsSelected to false
                        block.IsSelected = false;

                        //Remove that Block from the SelectedBlocks List
                        SelectedBlocks.Remove(block);

                    }

                    //If the user clicked a NodeBlock without clicking any other buttons, Unselect all NodeBlocks and Select this NodeBlock
                    else if (r.VisualHit.GetType() == typeof(NodeBlock))
                    {
                        //Get the NodeBlock that the User has Selected
                        NodeBlock block = r.VisualHit as NodeBlock;

                        //UnSelect All NodeBlocks
                        UnSelectAllNodeBlocks();

                        //Set the Selection of the NodeBlock that the User has Clicked to true
                        block.IsSelected = true;

                        //Add that Block to the List of SelectedBlocks
                        SelectedBlocks.Add(block);
                    }

                }
            }

        }


        #endregion



        #region Properties

        /// <summary>
        /// The boolean which determines wheather the grid is visible or not
        /// </summary>
        public bool IsGridVisible
        {
            get
            {
                return isGridVisible;
            }
            set
            {
                isGridVisible = value;
                //If the Visibilty of the Grid was True, then set the BackGround of the Canvas to a Grid BackGround
                if (isGridVisible == true)
                {
                    this.Background = new DrawingBrush()
                    {
                        //Set the DrawingBrush TileMode to Tile to make sure the Grid Squares repeat
                        TileMode = TileMode.Tile,
                        Viewport = new System.Windows.Rect(-10, -10, 40, 40),

                        //Set the ViewPort Units to Absoulute to Make sure Grid Size does not change with the window
                        ViewportUnits = BrushMappingMode.Absolute,

                        //Now Create the Geometry of the Grid Squares
                        Drawing = new GeometryDrawing()
                        {
                            //Set the Geometry to a square with a area of 40x40
                            Geometry = new RectangleGeometry() { Rect = new System.Windows.Rect(0, 0, 40, 40) },

                            
                            Pen = new Pen()
                            {
                                //Set the Color of the Grid Square to a dark gray Color
                                Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E292C")),

                                //Set the thickness of the Lines forming the square to 1
                                Thickness = 1
                            }
                        }
                    };
                }
                else
                {
                    //If the IsGridVisible was false, set the BackGround Color to a Solid Dark Grey Color
                    this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E292C"));
                }
            }
        }

        #endregion

    }
}
