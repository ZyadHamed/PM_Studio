using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Controls;

namespace PM_Studio
{
    public class FeaturesHeader : TreeViewItem
    {

        #region Variables

        private string headerText;

        #endregion

        #region Constructor

        public FeaturesHeader(string _HeaderText)
        {
            HeaderText = _HeaderText;
            this.Foreground = System.Windows.Media.Brushes.WhiteSmoke;
            this.FontSize = 20;
        }

        #endregion

        #region Properties

        public string HeaderText
        {
            get
            {
                return headerText;
            }

            set
            {
                headerText = value;
                this.Header = value;
            }
        }

        #endregion

    }
}
