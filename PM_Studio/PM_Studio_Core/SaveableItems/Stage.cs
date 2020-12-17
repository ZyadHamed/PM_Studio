using System;
using System.Collections.Generic;

namespace PM_Studio
{
    [Serializable]
    public class Stage
    {
        public string Version { get; set; }
        public string StageType { get; set; }
        public List<Feature> Features { get; set; }
        public List<BugToFix> BugsToFix { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
