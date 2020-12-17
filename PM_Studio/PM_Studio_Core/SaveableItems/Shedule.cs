using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    public class Shedule
    {

        public string Name { get; set; }
        public List<Task> Tasks { get; set; }

    }
}
