using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    public class Stage
    {
        public string Version { get; set; }
        public string StageType { get; set; }
        public List<Feature> Features { get; set; }
        public List<string> BugsToFix { get; set; }
    }
}
