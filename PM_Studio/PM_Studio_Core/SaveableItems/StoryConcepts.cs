using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    class StoryConcepts
    {
        public string[] StoryTopics { get; set; }
        public string[] StoryTypes { get; set; }
        public string[] PlotTwists { get; set; }
        public string[] PlotPoints { get; set; }
        public string[] StoryMinorSituations { get; set; }
    }
}
