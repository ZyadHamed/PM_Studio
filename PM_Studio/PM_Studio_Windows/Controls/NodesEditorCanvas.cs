using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PM_Studio
{
    public class NodesEditorCanvas : Canvas
    {

        //NodeBlock block1;
        
        public NodesEditorCanvas()
        {

            Line line1 = new Line();
            Line line2 = new Line();
            line1.Stroke = Brushes.Black;
            line2.Stroke = Brushes.Black;
            this.Background = Brushes.Blue;
            NodeBlock block1 = new NodeBlock("New Node",this);
            
            this.Children.Add(block1);
            this.Children.Add(line1);
            this.Children.Add(line2);
            block1.ToLine = line1;

            NodeBlock block2 = new NodeBlock("New Node2", this);
            this.Children.Add(block2);
            block2.FromLine = line1;
            block2.ToLine = line2;
            //block2.line.ToBlock = null;

            NodeBlock block3 = new NodeBlock("New Node3", this);
            this.Children.Add(block3);
            block3.FromLine = line2;
            

            Canvas.SetLeft(block1, 150);
            Canvas.SetTop(block1, 200);

            Canvas.SetLeft(block2, 200);
            Canvas.SetTop(block2, 300);

            Canvas.SetLeft(block3, 300);
            Canvas.SetTop(block3, 400);

        }
        
        

        
    }
}
