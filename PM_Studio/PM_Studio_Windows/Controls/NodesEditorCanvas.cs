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

        List<string> blocks = new List<string>() { "Block1", "Block2", "Block3", "Block4", "Block5", "Block6" };

        public NodesEditorCanvas()
        {

            this.Background = Brushes.Blue;

            FillCanvas(blocks);

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
                Random random = new Random();
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
                if(i + 1 < blocks.Count)
                {
                    Connect(blocks[i], blocks[i + 1]);
                }
            }
        }

    }
}
