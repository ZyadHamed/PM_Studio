using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PM_Studio
{
    public class NodesEditorCanvas : Canvas
    {
        #region Variables

        List<NodeBlock> SelectedBlocks = new List<NodeBlock>();

        private bool isGridVisible = false;
        private List<Node> nodes;

        #endregion

        #region Designing Variables

        ContextMenu menu = new ContextMenu();
        MenuItem NewNodeItem = new MenuItem();
        MenuItem ConnectNodesItem = new MenuItem();

        #endregion

        #region Constructor

        public NodesEditorCanvas(List<Node> _nodes)
        {
            this.MouseDown += NodesEditorCanvas_MouseDown;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E292C"));
            SetContextMenu();
            Nodes = _nodes;
            FillCanvasFromList(Nodes);

        }

        #endregion

        #region Designing Methods

        void SetContextMenu()
        {
            NewNodeItem.Header = "New Node";
            ConnectNodesItem.Header = "Connect Nodes";

            NewNodeItem.Click += MenuItem_Click;
            ConnectNodesItem.Click += MenuItem_Click;

            menu.Items.Add(NewNodeItem);
            menu.Items.Add(ConnectNodesItem);

            this.ContextMenu = menu;
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
            FirstBlock.Node.ToNodeText = SecondBlock.Node.Text;

            //Add that Arrow to the List of the FromLines of the Second Block(such that the Arrow Exits FirstBlock and Enters Second Block)
            SecondBlock.FromArrows.Add(arrow);

            //Reset the Arrows Position so that they match the coordinates of the blocks
            FirstBlock.SetArrowsPostion();
            SecondBlock.SetArrowsPostion();
        }

        /// <summary>
        /// Create NodeBlocks and Connect them to each other using a List of Nodes
        /// </summary>
        /// <param name="Nodes">The Nodes in which the NodeSystem is Based on</param>
        void FillCanvasFromList(List<Node> Nodes)
        {
            //Loop Inside Each Node in the Nodes List
            foreach(Node node in Nodes)
            {
                //Create a NodeBlock using that Node's Text
                NodeBlock block = new NodeBlock(node);
                //Add that NodeBlock to the Canvas
                this.Children.Add(block);

                //Create a Random Variable that Ranges between 2 and 800 for creating a random width and between 2 and 600 for a random height
                System.Random random = new System.Random();
                int X1 = random.Next(2, 800);
                int Y1 = random.Next(2, 600);

                //Set the Coordinates of the block using the value from that random variable
                Canvas.SetLeft(block, X1);
                Canvas.SetTop(block, Y1);
            }

            //now Create a List containing all the NodeBlocks in the Canvas
            List<NodeBlock> nodeBlocks = GetNodeBlocks();

            //Loop inside each NodeBlock
            for (int i = 0; i < nodeBlocks.Count; i++)
            {
                //Check If there was a NodeBlock that Matches the ToNodeText of the Current NodeBlock (ToNodeText is the Text of the NodeBlock in which this NodeBlock is Connected to)
                NodeBlock matchingToBlock = nodeBlocks.Find(x => x.Node.Text == Nodes[i].ToNodeText);

                //If that matchingToBlock was not null, then it exists, then Connect the Current Block in the Loop with it
                if (matchingToBlock != null)
                {
                    Connect(nodeBlocks[i], matchingToBlock);
                }
            }
                
        }

        void CreateNewNodeBlock(string BlockText)
        {
            //Create a New Node with the String passed in 
            Node node = new Node()
            {
                Text = BlockText
            };
            //Create a Node Block from that Node and Add it to the Canvas
            NodeBlock block = new NodeBlock(node);
            this.Children.Add(block);

            //Create a Random Variable that Ranges between 2 and 800 for creating a random width and between 2 and 600 for a random height
            System.Random random = new System.Random();
            int X1 = random.Next(2, 800);
            int Y1 = random.Next(2, 600);

            //Set the Coordinates of the block using the value from that random variable
            Canvas.SetLeft(block, X1);
            Canvas.SetTop(block, Y1);
        }

        /// <summary>
        /// Gets all the NodeBlocks in the Canvas
        /// </summary>
        /// <returns>a List Containing all the NodeBlocks in the Canvas</returns>
        List<NodeBlock> GetNodeBlocks()
        {
            //Create an Empty List of nodeBlocks
            List<NodeBlock> nodeBlocks = new List<NodeBlock>();
            
            //loop inside each Contorl in the Children of the Canvas
            foreach (var Control in this.Children)
            {
                //If the type of the Control was a NodeBlock, Add it to the List
                if(Control.GetType() == typeof(NodeBlock))
                {
                    nodeBlocks.Add(Control as NodeBlock);
                }
            }

            //Return the List of the nodeBlocks
            return nodeBlocks;
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

        public List<Node> GetNodes()
        {
            List<Node> nodes = new List<Node>();
            List<NodeBlock> blocks = GetNodeBlocks();

            foreach(NodeBlock block in blocks)
            {
                nodes.Add(block.Node);
            }

            return nodes;
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

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((sender as MenuItem).Header.ToString() == "New Node")
            {
                Create_ModifyItemsWindow window = new Create_ModifyItemsWindow(1);
                window.lbDataField2Text = "Node Text: ";
                if(window.ShowDialog() == true)
                {
                    CreateNewNodeBlock(window.txtDataField2Text);
                }

            }

            else if ((sender as MenuItem).Header.ToString() == "Connect Nodes")
            {
                for(int i = 0; i< SelectedBlocks.Count; i++)
                {
                    if(i + 1 < SelectedBlocks.Count)
                    {
                        SelectedBlocks[i].Node.ToNodeText = SelectedBlocks[i + 1].Node.Text;
                        Connect(SelectedBlocks[i], SelectedBlocks[i + 1]);
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

        public List<Node> Nodes
        {
            get
            {
                return nodes;
            }

            set
            {
                nodes = value;
            }
        }

        #endregion

    }
}
