using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    public class StoryConcepts
    {
        public string[] StoryTypes { get; set; }
        public string StoryIdea { get; set; }
        public string PlotTwists { get; set; }
        public string PlotPoints { get; set; }
        public string StoryEvents { get; set; }
    }
}
