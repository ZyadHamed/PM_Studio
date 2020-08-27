using System;
using System.Collections.Generic;
using System.Text;

namespace PM_Studio
{
    [Serializable]
    public class Team
    {
        public string TeamName { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}
