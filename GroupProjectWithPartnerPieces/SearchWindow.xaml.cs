using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.OleDb;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Collections;
using System.Reflection;


namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {

        #region Attributes
        clsDataAccess getData;

        SearchBusinessLogic sbl;

        //variable used by the main window
        public string sInvoiceNum;
        #endregion
        #region Methods
        public SearchWindow()
        {
            InitializeComponent();

            getData = new clsDataAccess();
            sbl = new SearchBusinessLogic();

            //populates the combo boxes
            for (int i = 0; i < sbl.getInvoiceAmounts(); i++)
            {
                InvoiceNumber.Items.Add(sbl.GetInvoice(i));
            }

            for (int i = 0; i < sbl.getDateAmounts(); i++)
            {
                InvoiceDate.Items.Add(sbl.GetDate(i));
            }

            for (int i = 0; i < sbl.getChargeAmounts(); i++)
            {
                TotalCharge.Items.Add(sbl.GetCharge(i));
            }

            //establishes the datagrid for first time use.
            DataSet test = new DataSet();
            test = sbl.ResetDataGrid();
            InvoiceGrid.ItemsSource = test.Tables[0].DefaultView;
            InvoiceGrid.CanUserAddRows = false;
        }

        /// <summary>
        /// Method for when the Cancel button is clicked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sInvoiceNum = "-1";
                this.Hide();
                //sets invoiceNum to null to make sure the main winow knows nothing was selected.
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method that runs when a new selection is made in the InvoiceNumber combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //this code establishes a dataset, assigns the current selections in all combo boxes, then runs a SQL query of all combo boxes to keep the datagrid changing in real time.
                DataSet clickEventData = new DataSet();
                string invoiceSelect = sbl.getComboBoxValue(InvoiceNumber.SelectedItem);
                string date1 = sbl.getComboBoxValue(InvoiceDate.SelectedItem);
                string chargeSelect = sbl.getComboBoxValue(TotalCharge.SelectedItem);
                clickEventData = sbl.UpdateDataGrid(invoiceSelect, date1, chargeSelect);
                InvoiceGrid.ItemsSource = clickEventData.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method Method that runs when a new selection is made in the InvoiceDate combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //this code establishes a dataset, assigns the current selections in all combo boxes, then runs a SQL query of all combo boxes to keep the datagrid changing in real time.
                DataSet clickEventData = new DataSet();
                string invoiceSelect = sbl.getComboBoxValue(InvoiceNumber.SelectedItem);
                string date1 = sbl.getComboBoxValue(InvoiceDate.SelectedItem);
                string chargeSelect = sbl.getComboBoxValue(TotalCharge.SelectedItem);
                clickEventData = sbl.UpdateDataGrid(invoiceSelect, date1, chargeSelect);
                InvoiceGrid.ItemsSource = clickEventData.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }


        /// <summary>
        /// Method Method that runs when a new selection is made in the TotalCharge combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TotalCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //this code establishes a dataset, assigns the current selections in all combo boxes, then runs a SQL query of all combo boxes to keep the datagrid changing in real time.
                DataSet clickEventData = new DataSet();
                string invoiceSelect = sbl.getComboBoxValue(InvoiceNumber.SelectedItem);
                string date1 = sbl.getComboBoxValue(InvoiceDate.SelectedItem);
                string chargeSelect = sbl.getComboBoxValue(TotalCharge.SelectedItem);
                clickEventData = sbl.UpdateDataGrid(invoiceSelect, date1, chargeSelect);
                InvoiceGrid.ItemsSource = clickEventData.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method that handles what to do when the user selects SelectInvoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //checks if any selection was actually made. If so, it hides since sInvoiceNum already is set to which invoice was selected. 
                if (sInvoiceNum == null)
                {
                    MessageBox.Show("You must select an invoice");
                }
                else if (sInvoiceNum != null)
                {
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method that handles when the user selects a value from the InvoiceGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //creates a datarowview that store the currently selected row
                DataRowView drv = (DataRowView)InvoiceGrid.SelectedItem;
                //sets sInvoiceNum to the InvoiceNum column of the currently selected row
                sInvoiceNum = (drv["InvoiceNum"]).ToString();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method that handles when the user clicks ResetSelection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetSelection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this code creates a dataset, resets the data grid by doing a SELECT * FROM Invoices query, then sets all combo boxes and sInvoiceNum to null
                DataSet clickEventData = new DataSet();
                clickEventData = sbl.ResetDataGrid();
                InvoiceGrid.ItemsSource = clickEventData.Tables[0].DefaultView;
                sInvoiceNum = null;
                InvoiceNumber.SelectedItem = null;
                InvoiceDate.SelectedItem = null;
                TotalCharge.SelectedItem = null;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// try catch error handler
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + "->" + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
        #endregion
    }



}

    

