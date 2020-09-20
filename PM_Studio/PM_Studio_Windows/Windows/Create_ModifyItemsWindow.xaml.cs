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
using System.Windows.Shapes;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for Create_ModifyItemsWindow.xaml
    /// </summary>
    public partial class Create_ModifyItemsWindow : Window
    {
        public Create_ModifyItemsWindow(int DataEntriesNumber)
        {
            InitializeComponent();

            if (DataEntriesNumber == 1)
            {
                lbDataField1.Visibility = Visibility.Hidden;
                txtDataField1.Visibility = Visibility.Hidden;

                lbDataField3.Visibility = Visibility.Hidden;
                txtDataField3.Visibility = Visibility.Hidden;
            }

            else if (DataEntriesNumber == 2)
            {
                lbDataField2.Visibility = Visibility.Hidden;
                txtDataField2.Visibility = Visibility.Hidden;
            }

            else if(DataEntriesNumber == 3)
            {

            }
        }

        #region Properties

        public string lbDataField1Text
        {
            get
            {
                return lbDataField1.Text;
            }
            set
            {
                lbDataField1.Text = value;
            }
        }

        public string txtDataField1Text
        {
            get
            {
                return txtDataField1.Text;
            }
            set
            {
                txtDataField1.Text = value;
            }
        }

        public string lbDataField2Text
        {
            get
            {
                return lbDataField2.Text;
            }
            set
            {
                lbDataField2.Text = value;
            }
        }

        public string txtDataField2Text
        {
            get
            {
                return txtDataField2.Text;
            }
            set
            {
                txtDataField2.Text = value;
            }
        }

        public string lbDataField3Text
        {
            get
            {
                return lbDataField3.Text;
            }
            set
            {
                lbDataField3.Text = value;
            }
        }

        public string txtDataField3Text
        {
            get
            {
                return txtDataField3.Text;
            }
            set
            {
                txtDataField3.Text = value;
            }
        }

        #endregion

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Set the Dialog Result of the Form to Ok
            this.DialogResult = true;
        }
    }
}
