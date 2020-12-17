using System;

namespace PM_Studio
{
    [Serializable]
    public class BugToFix
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
