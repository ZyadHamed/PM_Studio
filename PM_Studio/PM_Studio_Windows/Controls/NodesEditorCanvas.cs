using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PM_Studio
{
    public class NodesEditorCanvas : Canvas
    {
        
        
        public NodesEditorCanvas()
        {

            this.Background = Brushes.Blue;

            //Create 4 NodeBlocks and Add them to the Canvas
            NodeBlock block1 = new NodeBlock("New Node", this);
            this.Children.Add(block1);

            NodeBlock block2 = new NodeBlock("New Node2", this);
            this.Children.Add(block2);

            NodeBlock block3 = new NodeBlock("New Node3", this);
            this.Children.Add(block3);

            NodeBlock block4 = new NodeBlock("New Node4", this);
            this.Children.Add(block4);

            //Add Some Distance between the blocks and the (0,0) point of the Canvas
            Canvas.SetLeft(block1, 150);
            Canvas.SetTop(block1, 200);

            Canvas.SetLeft(block2, 200);
            Canvas.SetTop(block2, 300);

            Canvas.SetLeft(block3, 300);
            Canvas.SetTop(block3, 400);

            Canvas.SetLeft(block4, 350);
            Canvas.SetTop(block4, 450);

            //Connect all the Blocks
            Connect(block1, block2);
            Connect(block2, block3);
            Connect(block3, block4);

        }

        /// <summary>
        /// Connects 2 Blocks with Each Other
        /// </summary>
        /// <param name="FirstBlock">The First Block</param>
        /// <param name="SecondBlock">The Second Block</param>
        void Connect(NodeBlock FirstBlock, NodeBlock SecondBlock)
        {
            //Create the Line that will connect both blocks and set it's color to black
            Line line = new Line();
            line.Stroke = Brushes.Black;

            //Add the Line to the Canvas
            this.Children.Add(line);

            //Set the ToLine of the First Block to that Line
            FirstBlock.ToLine = line;

            //Set the FromLine of the Second Block to that Line(such that a Line Exits FirstBlock and Enters Second Block)
            SecondBlock.FromLine = line;
        }

        
        
    }
}
