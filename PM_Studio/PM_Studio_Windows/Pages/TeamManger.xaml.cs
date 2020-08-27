using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for TeamManger.xaml
    /// </summary>
    public partial class TeamManger : Page
    {
        Team team;
        public TeamManger(Team _team)
        {
            team = _team;
            InitializeComponent();
        }
    }
}
