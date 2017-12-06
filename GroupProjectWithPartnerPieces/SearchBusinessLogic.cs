using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Data;
using System.Windows.Controls;
using System.Reflection;

namespace FinalProject
{
    class SearchBusinessLogic
    {
        #region Attributes
        clsDataAccess getData;
        #endregion

        #region Methods
        /// <summary>
        /// Method that resets the datagrid to reflect all values in the database.
        /// </summary>
        /// <returns></returns>
        public DataSet ResetDataGrid()
        {
            try
            {
                //creates a dataset, queries all values from the database, fills the dataset with the results then returns the dataset. 
                getData = new clsDataAccess();
                DataSet resetGrid = new DataSet();
                string sSQL = "SELECT * FROM Invoices";
                int iRet = 0;
                resetGrid = getData.ExecuteSQLStatement(sSQL, ref iRet);
                return resetGrid;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Updates the data grid based on the combo box selections made by the user. 
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="date"></param>
        /// <param name="charge"></param>
        /// <returns></returns>
        public DataSet UpdateDataGrid(string invoice, string date, string charge)
        {
            try
            {
                //creates a dataset, a blank SQL string, then checks what conditions the combo boxes are in. Based on the combo box conditions it will select the appropriate query with the data selected by the user. 
                getData = new clsDataAccess();
                DataSet UpdatedGrid = new DataSet();
                string sSQL = "";
                int iRet = 0;
                if (invoice == null || date == null || charge == null)
                {
                    sSQL = "SELECT  * FROM Invoices";
                }
                else if (invoice != "" && date == "" && charge == "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceNum = " + invoice;
                }

                else if (invoice == "" && date != "" && charge == "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceDate = #" + date + "#";
                }

                else if (invoice == "" && date == "" && charge != "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE TotalCharge = " + charge;
                }

                else if (invoice != "" && date != "" && charge == "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceNum = " + invoice + " AND InvoiceDate = #" + date + "#";
                }

                else if (invoice == "" && date != "" && charge != "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceDate = #" + date + "#" + " AND TotalCharge =" + charge;
                }

                else if (invoice != "" && date == "" && charge != "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceNum = " + invoice + " AND TotalCharge = " + charge;
                }

                else if (invoice != "" && date != "" && charge != "")
                {
                    sSQL = "SELECT  * FROM Invoices WHERE InvoiceNum = " + invoice + " AND InvoiceDate = #" + date + "#" + " AND TotalCharge = " + charge;
                }

                else if (invoice == "" && date == "" && charge == "")
                {
                    sSQL = "SELECT * FROM Invoices";

                }
                //runs query then returns result. 
                UpdatedGrid = getData.ExecuteSQLStatement(sSQL, ref iRet);
                return UpdatedGrid;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }
        /// <summary>
        /// returns the value of the current selection of the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public string getComboBoxValue(object sender)
        {
            try
            {
                //checks if value is null (if the user hasnt selected anything in a particular combo box), then returns the given value. Checks for null to avoid database query exceptions. 
                string value;
                if (sender == null)
                {
                    value = "";

                }
                else
                {
                    value = sender.ToString();
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the total count of Invoice numbers from the database to populate upon loading of the window. 
        /// </summary>
        /// <returns></returns>
        public int getInvoiceAmounts()
        {
            try
            {
                //creates a dataset, runs a query for all invoice numbers, then returns the count of the total number of invoice numbers. 
                getData = new clsDataAccess();
                DataSet amountInvoice = new DataSet();
                int iRet = 0;
                string sSQL = "SELECT InvoiceNum FROM Invoices";
                amountInvoice = getData.ExecuteSQLStatement(sSQL, ref iRet);
                return iRet;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the total count of Invoice dates from the database to populate upon loading of the window. 
        /// </summary>
        /// <returns></returns>
        public int getDateAmounts()
        {
            try
            {
                //creates a dataset, runs a query for all invoice dates, then returns the count of the total number of invoice dates.
                getData = new clsDataAccess();
                DataSet amountInvoice = new DataSet();
                int num = 0;
                string sSQL = "SELECT DISTINCT  InvoiceDate FROM Invoices";
                amountInvoice = getData.ExecuteSQLStatement(sSQL, ref num);
                return num;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the total count of Invoice charge amounts from the database to populate upon loading of the window.
        /// </summary>
        /// <returns></returns>
        public int getChargeAmounts()
        {
            try
            {
                //creates a dataset, runs a query for all invoice charge amounts, then returns the count of the total number of invoice charge amounts.
                getData = new clsDataAccess();
                DataSet amountInvoice = new DataSet();
                int num = 0;
                string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices";
                amountInvoice = getData.ExecuteSQLStatement(sSQL, ref num);
                return num;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the currently selected invoice number to populate in the combo box when the window first opens.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public int GetInvoice(int invoiceNum)
        {
            try
            {
                //creates a dataset and a SQL query, then queries for the currently selected invoice number, parses it, then returns it to the window.
                getData = new clsDataAccess();
                DataSet GetInvoiceDB = new DataSet();
                int num = 0;
                int Intresult = 0;
                string sSQL = "SELECT InvoiceNum FROM Invoices";
                GetInvoiceDB = getData.ExecuteSQLStatement(sSQL, ref num);
                string result = GetInvoiceDB.Tables[0].Rows[invoiceNum][0].ToString();
                Int32.TryParse(result, out Intresult);
                return Intresult;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the currently selected invoice date to populate in the combo box when the window first opens.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string GetDate(int DateNum)
        {
            try
            {
                //creates a dataset and a SQL query, then queries for the currently selected invoice date, then returns it to the window.
                getData = new clsDataAccess();
                DataSet GetDateDB = new DataSet();
                int iRet = 0;
                string sSQL = "SELECT DISTINCT Format(InvoiceDate, 'mm/dd/yyyy') FROM Invoices";
                GetDateDB = getData.ExecuteSQLStatement(sSQL, ref iRet);
                string DateResult = GetDateDB.Tables[0].Rows[DateNum][0].ToString();
                return DateResult;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Gets the currently selected invoice charge amount to populate in the combo box when the window first opens.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public double GetCharge(int ChargeNum)
        {
            try
            {
                //creates a dataset and a SQL query, then queries for the currently selected invoice date, parses it to a double, then returns it to the window.
                getData = new clsDataAccess();
                DataSet GetChargeDB = new DataSet();
                double charge = 0.0;
                int iRet = 0;
                string sSQL = "SELECT DISTINCT TotalCharge FROM Invoices ";
                GetChargeDB = getData.ExecuteSQLStatement(sSQL, ref iRet);
                string result = GetChargeDB.Tables[0].Rows[ChargeNum][0].ToString();
                Double.TryParse(result, out charge);
                return charge;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }
        #endregion
    }
}
