using System;
using System.Collections.Generic;

namespace PM_Studio
{
    [Serializable]
    public class NodeSystem
    {
        public string fileName { get; set; }
        public List<Node> Nodes { get; set; }
        
    }
}
