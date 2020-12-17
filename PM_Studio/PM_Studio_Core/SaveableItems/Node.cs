using System;

namespace PM_Studio
{
    [Serializable]
    public class Node
    {
        public string Text { get; set; }

        public string ToNodeText { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
