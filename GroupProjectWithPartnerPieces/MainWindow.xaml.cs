using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Creates an object of type clsDataAccess to access the database
        /// </summary>
        clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// creates a variable name for the sql class
        /// </summary>
        clsSQL mydb = new clsSQL();

        /// <summary>
        /// creates a variable name for invoiceNum
        /// </summary>
        String invoiceId;

        /// <summary>
        /// dictionary for the inventory
        /// </summary>
        Dictionary<String, String> inventoryDictionary;

        /// <summary>
        /// data table for inventory
        /// </summary>
        DataTable dtInventory, dtInvoice = new System.Data.DataTable();

        /// <summary>
        /// searchWindow object
        /// </summary>
        SearchWindow searchWin;

        /// <summary>
        /// editWindow object
        /// </summary>
        EditWindow edtWindow;

        /// <summary>
        /// used to keep get InvoiceID
        /// </summary>
        String sInvoiceNum;

        /// <summary>
        /// main window initialization
        /// </summary>
        public MainWindow()
        {

            ///initialize the window
            InitializeComponent();

            ///invoice picker method
            invoiceDatePicker.SelectedDate = DateTime.Now.Date;

            ///inventory dictionary
            inventoryDictionary = new Dictionary<String, String>();

            //this.invoiceId = clsUtil.invoiceId;
            invoiceId = ""; //set to empty string. Methods below check this condition

            ///populates the inventory grid
            populateInventory();

            ///this is needed to set up the table so the inventory items can be added
            populateInvoice(invoiceId);

            ///calculate total variable
            calculateTotal();
        }

        /// <summary>
        /// Search Window Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Window_Click(object sender, RoutedEventArgs e)
        {
            ///try catch block to catch errors
            try
            {
                ///instantiates search window
                searchWin = new SearchWindow();

                ///launches the searchWindow to find and retrieve InvoiceID
                searchWin.ShowDialog();

                ///sets the returned InvoiceID
                if(searchWin.sInvoiceNum == null)
                {
                    return;
                }
                else
                {
                    sInvoiceNum = searchWin.sInvoiceNum;
                }

                ///writes the invoice ID 
                Console.WriteLine("InvoiceID = " + sInvoiceNum);

                ///invoice id takes in invoiceNum
                invoiceId = sInvoiceNum;

                ///populates the invoice grid with user selected invoice
                populateInvoice(invoiceId);

                ///calculatse total method
                calculateTotal();

                ///closes search window
                searchWin.Close();
            }
            catch (Exception)///catch exceptions
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch

        }//end search click


        /// <summary>
        /// opens edit inventory window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Creates an EditWindow object
                edtWindow = new EditWindow();
                // Shows the edit window
                edtWindow.ShowDialog();
                // Closes the main window
                edtWindow.Close();

                //inventoryDictionary = new Dictionary<string, string>();
                //populateInventory();
            }
            catch (Exception)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch
        }//end inventory click


        /// <summary>
        /// gets the inventory data from the database.
        /// I am also creating a dictionary to refer back to. I use this in the Add/Update click event.
        /// This prevents additional reads to the database to retrieve the ItemCode.
        /// </summary>
        public void populateInventory()
        {
            ///try catch block to handle exceptions
            try
            {
                //gets data from database and load into dgInventoryItems
                //Method should be run in initialize method

                ///sQuery for inventory items
                String sQuery = mydb.SelectInventoryItems();
                ///database query
                dtInventory = db.FillSqlDataTable(sQuery);
                ///inventory items set to default view 
                dgInventoryItems.ItemsSource = dtInventory.DefaultView;
                ///foreach loop to populate the grid
                foreach (DataRow row in dtInventory.Rows)
                {
                    inventoryDictionary.Add(row[1].ToString(), row[0].ToString());
                }
            }
            catch (Exception)///catch exceptions
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch

        }//end populateInventory()


        /// <summary>
        /// If there is an invoice number, get the data from the database and populate the datatable and dataset.
        /// If there is not in invoice given, set up the table to be able to accept inventory items.
        /// </summary>
        /// <param name="invoiceId"></param>
        public void populateInvoice(String invoiceId)
        {
            ///try catch block to handle exceptions
            try
            {
                ///if statement to check invoice ID and populate invoice
                if (invoiceId != "")
                {
                    ///sets delete invoice to enabled
                    btnDeleteInvoice.IsEnabled = true;
                    ///sets label with number
                    lblInvoiceNumber.Content = invoiceId;
                    ///creates a string to hold invoice id info
                    String sQuery = mydb.SelectItemsOnInvoice(invoiceId);
                    ///executes query
                    dtInvoice = db.FillSqlDataTable(sQuery);
                    ///populates grid
                    dgInvoiceItems.ItemsSource = dtInvoice.DefaultView;
                    ///creates a return variable
                    int iRet = 0;
                    ///creates a data set
                    DataSet ds = new DataSet();
                    ///calls a sql method
                    String sSQL = mydb.SelectInvoiceDateFromNum(invoiceId);
                    ///places info in data set
                    ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                    ///creates temp variable to hold info
                    String date = ds.Tables[0].Rows[0][0].ToString();
                    ///takes care of the date format
                    invoiceDatePicker.SelectedDate = DateTime.Parse(date);


                    //pulls data from database
                    //UPDATE INVOICE LABEL WITH INVOICEID - lblInvoiceId
                }
                else
                {
                    ///if all the above fails, then do this
                    btnDeleteInvoice.IsEnabled = false;
                    //fields should be clear and ready for input.
                    dtInvoice.Columns.Add("ItemDesc");
                    dtInvoice.Columns.Add("Cost");
                    ///populatse invoice items
                    dgInvoiceItems.ItemsSource = dtInvoice.DefaultView;
                }//end else
            }
            catch (Exception)
            {///catch exceptions

                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch

        }//end populateInvoice()


        /// <summary>
        /// adds the selected inventory item to the invoice datatable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddInventory_Click(object sender, RoutedEventArgs e)
        {
            ///try catch block
            try
            {
                ///creates datarow for inventory items
                DataRowView dataRow = (DataRowView)dgInventoryItems.SelectedItem;
                //int index = dgInventoryItems.CurrentCell.Column.DisplayIndex;
                ///this below captures certain cells of info
                string item = dataRow.Row.ItemArray[1].ToString();
                string cost = dataRow.Row.ItemArray[2].ToString();
                DataRow dr = dtInvoice.NewRow();
                dr[0] = item;
                dr[1] = cost;
                dtInvoice.Rows.Add(dr);
                calculateTotal();
                //dgInvoiceItems.Items.Add(new Item {item = dataRow.Row.ItemArray[0].ToString(), price = dataRow.Row.ItemArray[1].ToString() });
            }
            catch (Exception)
            {///catch exception

                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch
        }//end btnAddInventory

        /// <summary>
        /// removes selected index of the invoice datatable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            ///creates variable index to remove items
            int index = dgInvoiceItems.SelectedIndex;
            ///calls remove function
            dtInvoice.Rows.RemoveAt(index);
            ///calls calculate total method
            calculateTotal();
        }//end remove item

        /// <summary>
        /// if given invoice id Updates the date and total charge. delete everything in the LineItem table, and write the data again with the data in the datatable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ///if statement to check and compare dates
                if (invoiceDatePicker.SelectedDate == null) { invoiceDatePicker.SelectedDate = DateTime.Now.Date; }// if no date is picked, default to today
                int count = 0;//keeps track of records inserted
                ///if invoice id is null
                if (invoiceId != "")
                {

                    //updates the date, even if there were no changes. 
                    String sSQL = mydb.updateInvoiceDate(invoiceDatePicker.SelectedDate.Value.ToShortDateString(), invoiceId);
                    db.ExecuteNonQuery(sSQL);
                    System.Console.WriteLine(sSQL);

                    //updates the cost of the associated invoiceId
                    sSQL = mydb.updateTotalCharge(calculateTotal() + "", invoiceId);
                    db.ExecuteNonQuery(sSQL);

                    System.Console.WriteLine(sSQL);

                    //removing all LineItems associated with that invoice number, and adding them again with the added/removed items
                    sSQL = mydb.DeleteLineItems(invoiceId);
                    db.ExecuteNonQuery(sSQL);


                    //grabs the data from the DataTable, runs a sql statement adding each line individually.
                    for (int i = 0; i < dtInvoice.Rows.Count; i++)
                    {
                        sSQL = mydb.addLineItem(invoiceId, i + 1 + "", inventoryDictionary[dtInvoice.Rows[i][0] + ""]);
                        System.Console.WriteLine(sSQL);
                        db.ExecuteNonQuery(sSQL);
                        count++;
                    }

                    MessageBox.Show("Invoice: " + invoiceId + " added successfully!\n" + count + " items added");

                }
                else
                {

                    String sSQL = mydb.addInvoice(invoiceDatePicker.SelectedDate.Value.ToShortDateString(), calculateTotal() + "");
                    db.ExecuteNonQuery(sSQL);
                    sSQL = mydb.latestInvoice();
                    invoiceId = db.ExecuteScalarSQL(sSQL);

                    for (int i = 0; i < dtInvoice.Rows.Count; i++)
                    {
                        sSQL = mydb.addLineItem(invoiceId, i + 1 + "", inventoryDictionary[dtInvoice.Rows[i][0] + ""]);
                        System.Console.WriteLine(sSQL);
                        db.ExecuteNonQuery(sSQL);
                        count++;//keeps track of items added.
                    }//end for

                    MessageBox.Show("Invoice: " + invoiceId + " added successfully!\n" + count + " items added");


                }//end else
            }
            catch (Exception)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch

        }//end add/update click

        /// <summary>
        /// delete invoice button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            ///try catch block
            try
            {
                ///check to see what the message box is showing
                if (MessageBox.Show("Are you sure you want to delete Invoice Number: " + invoiceId + "?", "Delete Invoice?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    ///if that fails, do this
                    String sSQL = mydb.DeleteLineItems(invoiceId);
                    db.ExecuteNonQuery(sSQL);

                    sSQL = mydb.DeleteInvoice(invoiceId);
                    db.ExecuteNonQuery(sSQL);

                    //easiest way to reset all values is to open a new window.
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();

                }//end else
            }
            catch (Exception)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
            }//end catch
        }//end delete 


        /// <summary>
        /// calculates the total by getting the cost row in the invoice data table, and adding them together.
        /// </summary>
        /// <returns></returns>
        public double calculateTotal()
        {
            ///creates a double to hold total
            Double total = 0.00;
            ///if invoice is not null execute the calculate total
            if (dtInvoice != null)
            {
                try
                {
                    foreach (DataRow row in dtInvoice.Rows)
                    {
                        total += Double.Parse(row[1].ToString());
                    }//end try
                }
                catch (Exception)
                {
                    MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name);
                }//end catch
            }//end if
            ///updates label
            lblTotal.Content = "$" + total;
            ///returns total
            return total;
        }//end calculate total()

    }//end class

}//end namespace
