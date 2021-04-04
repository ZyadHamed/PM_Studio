using System;
using System.Windows;

namespace PM_Studio
{
    /// <summary>
    /// Interaction logic for Create_ModifyItemsWindow.xaml
    /// </summary>
    public partial class Create_ModifyItemsWindow : Window
    {
        
        public Create_ModifyItemsWindow(int DataEntriesNumber, bool IsDateTimePickerVisible = false)
        {
            InitializeComponent();
            IsDatePickerVisible = IsDateTimePickerVisible;
            //If the Number of DataEntries was 1, show only the TextBox and the Label in the Middle and hide the rest
            if (DataEntriesNumber == 1)
            {
                lbDataField2.Visibility = Visibility.Collapsed;
                txtDataField2.Visibility = Visibility.Collapsed;

                lbDataField3.Visibility = Visibility.Collapsed;
                txtDataField3.Visibility = Visibility.Collapsed;
                DatePickersContainer.Visibility = Visibility.Collapsed;
            }

            //If the Number of DataEntries was 2
            else if (DataEntriesNumber == 2)
            {
                //If the DateTimePicker boolean was true, show the datetime pickers and hide the text box
                if(IsDateTimePickerVisible == true) 
                {
                    txtDataField3.Visibility = Visibility.Collapsed;
                    lbDataField2.Visibility = Visibility.Collapsed;
                    txtDataField2.Visibility = Visibility.Collapsed;

                }

                //If the DateTimePicker boolean was false, hide the datetime pickers and show the text box
                else
                {
                    txtDataField3.Visibility = Visibility.Collapsed;
                    lbDataField3.Visibility = Visibility.Collapsed;
                    DatePickersContainer.Visibility = Visibility.Collapsed;
                }
            }

            //If the Number of DataEntries was 3
            else if (DataEntriesNumber == 3)
            {
                //If the DateTimePicker boolean was true, show the datetime pickers and hide the text box
                if (IsDateTimePickerVisible == true)
                {
                    txtDataField3.Visibility = Visibility.Collapsed;

                }

                //If the DateTimePicker boolean was false, hide the datetime pickers and show the text box
                else
                {
                    DatePickersContainer.Visibility = Visibility.Collapsed;
                }
            }

        }

        #region Properties

        /// <summary>
        /// The Text Inside the first Label
        /// </summary>
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

        /// <summary>
        /// The Text Inside the first TextBox
        /// </summary>
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

        /// <summary>
        /// The Text Inside the second Label
        /// </summary>
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

        /// <summary>
        /// The Text Inside the second TextBox
        /// </summary>
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

        /// <summary>
        /// The Text Inside the third Label
        /// </summary>
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

        /// <summary>
        /// The Text Inside the third TextBox
        /// </summary>
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

        /// <summary>
        /// The Start Date of the Task
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return dpStartDate.SelectedDate.Value;
            }
            set
            {
                dpStartDate.SelectedDate = value;
            }
        }

        /// <summary>
        /// The End Date of the Task
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return dpEndDate.SelectedDate.Value;
            }
            set
            {
                dpEndDate.SelectedDate = value;
            }
        }

        bool IsDatePickerVisible { get; set; }
        #endregion

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //If the DatePickers were Visible, do the Data and the Date Checking
            if (IsDatePickerVisible == true)
            {
                //If there Was missing Data, show an Error
                if (String.IsNullOrEmpty(txtDataField1.Text) || dpStartDate.SelectedDate.Value == null || dpEndDate.SelectedDate.Value == null)
                {
                    MessageBox.Show("Please fill All the Data in the Window");
                    return;
                }
                //else, Do the Date checking
                else
                {
                    //If the End Date was Earilier than the Start Date, Show an Error Message
                    if (DateTime.Compare(StartDate, EndDate) > 0)
                    {
                        MessageBox.Show("The Starting Date of the Task must Be Before the Finishing Date");
                        return;
                    }
                    else
                    {
                        ////If the Start date was Earilier than Today, Display Another Error Message
                        //if (DateTime.Compare(DateTime.Now, StartDate) >= 0)
                        //{
                        //    MessageBox.Show("The Starting Date Cannot Be Earlier than Today");
                        //    return;
                        //}
                        ////If not, Set the Dialog Result to Ok and Close the Form
                        //else
                        //{
                            //Set the Dialog Result of the Form to Ok
                            this.DialogResult = true;
                        //}
                    }
                }

            }
            else
            {
                //Set the Dialog Result of the Form to Ok
                this.DialogResult = true;
            }


        }


    }
}
