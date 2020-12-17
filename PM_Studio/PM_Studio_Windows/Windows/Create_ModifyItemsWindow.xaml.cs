using System;
using System.Windows;

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

            //If the Number of DataEntries was 1, show only the TextBox and the Label in the Middle
            if (DataEntriesNumber == 1)
            {
                lbDataField1.Visibility = Visibility.Hidden;
                txtDataField1.Visibility = Visibility.Hidden;

                lbDataField3.Visibility = Visibility.Hidden;
                txtDataField3.Visibility = Visibility.Hidden;
                DatePickersContainer.Visibility = Visibility.Hidden;
            }

            //If the Number of DataEntries was 2, show the TextBoxes and the Labels in the Top and the Bottom
            else if (DataEntriesNumber == 2)
            {
                //Hide the Middle TextBox And Label
                lbDataField2.Visibility = Visibility.Hidden;
                txtDataField2.Visibility = Visibility.Hidden;

                //Give the Middle Row a Height of 0 to prevent it from consuming space
                MiddleRow.Height = new GridLength(0);

                //Set the Vertical Alignment of the botton label to Top to make it match with the DatePickers
                lbDataField3.VerticalAlignment = VerticalAlignment.Top;
                //Decrease the Font size of the bottom label to make the text fit with the date pickers size
                lbDataField3.FontSize = 16;
                //Add some Margin to the Top textbox to give it a good width and height
                txtDataField1.Margin = new Thickness(5,30,5,30);
            }

            //If the Number of DataEntries was 3, Do nothing
            else if (DataEntriesNumber == 3)
            {

            }
            //Set the Visibilty of the DatePicker to False by default
            IsDatePickerVisible = false;

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
        /// The Bool which indicates wheather the DatePickers are Visible or not
        /// </summary>
        public bool IsDatePickerVisible
        {
            get
            {
                return DatePickersContainer.Visibility == Visibility.Visible;
            }
            set
            {
                if(DatePickersContainer.Visibility != Visibility.Visible || txtDataField3.Visibility != Visibility.Visible)
                {
                    if (value == true)
                    {
                        DatePickersContainer.Visibility = Visibility.Visible;
                        txtDataField3.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        DatePickersContainer.Visibility = Visibility.Hidden;
                        txtDataField3.Visibility = Visibility.Visible;
                    }

                }
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
