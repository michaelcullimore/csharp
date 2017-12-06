using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class clsSQL
    {

        /// <summary>
        /// This SQL gets all invoices
        /// </summary> 
        /// <returns>All data for the given invoice.</returns>
        public string populateAllInvoices()
        {
            string sSQL = "SELECT * FROM Invoices";
            return sSQL;
        }

        /// <summary>
        /// This SQL gets all the data on an invoice for a given Invoice ID
        /// </summary> 
        /// <param name="sInvoiceNum">The Invoice ID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceID(string sInvoiceNum)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }

        /// <summary>
        /// This SQL gets all the data on an invoice for a given InvoiceDate
        /// </summary> 
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceDate(string sInvoiceDate)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate= " + sInvoiceDate;
            return sSQL;
        }

        /// <summary>
        /// This SQL gets all the data on an invoice for a given total charge
        /// </summary> 
        /// <param name="sTotalCharge">The total charge for the invoice in order to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectTotalCharge(string sTotalCharge)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCharge= " + sTotalCharge;
            return sSQL;
        }


        /// <summary>
        /// Selects items from an invoice based on the invoice number given
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public String SelectItemsOnInvoice(string sInvoiceNum)
        {
            String sSQL = "SELECT ID.ItemDesc, ID.Cost " +
                          "FROM ItemDesc ID " +
                          "INNER JOIN(Invoices I INNER JOIN LineItems LI ON I.InvoiceNum = LI.InvoiceNum) ON ID.ItemCode = LI.ItemCode " +
                          "WHERE I.InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }//end SelectItemsOnInvoice()


        /// <summary>
        /// Selects inventory items and associated cost.
        /// </summary>
        /// <returns></returns>
        public String SelectInventoryItems()
        {
            String sSQL = "SELECT ItemDesc.ItemCode,ItemDesc.ItemDesc, ItemDesc.Cost " +
                          "FROM ItemDesc;";
            return sSQL;
        }//end SelectinventoryItems()


        /// <summary>
        /// gets the date of the selected invoice number so we can populate the datepicker.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public String SelectInvoiceDateFromNum(String invoiceNum)
        {
            String SQL = "SELECT Invoices.[InvoiceDate] " +
                         "FROM Invoices " +
                         "WHERE Invoices.InvoiceNum = " + invoiceNum + ";";

            return SQL;
        }//end SelectInvoiceDateFromNum

        /// <summary>
        /// Finds if item is in an invoice.
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public string CheckIfItemIsInAnInvoice(string ItemCode)
        {
            string sSQL = "SELECT DISTINCT ItemCode " +
                          "FROM LineItems " +
                          "WHERE ItemCode = " + ItemCode;
            return sSQL;
        }//end CheckIfItemIsAnInvoice

        /// <summary>
        /// Adds an inventory item to the database.
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        public string AddInventoryItem(string ItemDesc, string Cost)
        {
            string sSQL = "INSERT INTO ItemDesc(ItemDesc, Cost) " +
                          "VALUES('" + ItemDesc + "', " + Cost + ");";

            return sSQL;
        }//end AddInventoryItem

        /// <summary>
        /// Deletes an entry from the ItemDesc table.
        /// </summary>
        /// <param name="ItemCode">Primary Key for the ItemDesc table.</param>
        /// <returns></returns>
        public string DeleteInventoryItem(string ItemCode)
        {
            string sSQL = "DELETE FROM ItemDesc " +
                          "WHERE ItemCode = " + ItemCode;
            return sSQL;
        }//end DeleteInventoryItem

        /// <summary>
        /// Edit an entry from the ItemDesc table.
        /// </summary>
        /// <param name="ItemCode">primary key for the ItemDesc table.</param>
        /// <param name="ItemDesc">Contains the description of the item.</param>
        /// <param name="Cost">Contains the cost of the item.</param>
        /// <returns></returns>
        public string EditInventoryItem(string ItemCode, string ItemDesc, string Cost)
        {
            string sSQL = "UPDATE ItemDesc " +
                          "SET ItemDesc = '" + ItemDesc + "', Cost = " + Cost +
                          " WHERE ItemCode = " + ItemCode;
            return sSQL;
        }//end EditInventoryItem

        /// <summary>
        /// Delete an Invoice
        /// </summary>
        /// <param name="sDeleteInvoice"></param>
        /// <returns></returns>
        public string DeleteInvoice(string sInvoiceNum)
        {
            string sSQL = "DELETE * FROM Invoices WHERE invoiceNum = " + sInvoiceNum;
            return sSQL;
        }//end DeleteInvoice

        /// <summary>
        /// deletes all items from LineItems with associated invoice number
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItems(string sInvoiceNum)
        {
            string sSQL = "DELETE * FROM LineItems WHERE `invoiceNum` = " + sInvoiceNum;
            return sSQL;
        }//end DeleteLineItems


        /// <summary>
        /// inserts record into Invoice table
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCharge"></param>
        /// <returns></returns>
        public String addInvoice(String invoiceDate, String totalCharge)
        { //DATE TO BE IN FORMAT MM/DD/YYY
            String SQL = "INSERT INTO Invoices ( InvoiceDate, TotalCharge) VALUES ( #" + invoiceDate + "#, " + totalCharge + " );";

            return SQL;
        }//end addInvoice

        /// <summary>
        /// inserts record into LineItems
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string addLineItem(String invoiceNum, String lineItemNum, String itemCode)
        {
            String SQL = "INSERT INTO LineItems ( InvoiceNum, LineItemNum, ItemCode) VALUES (" + invoiceNum + ", " + lineItemNum + ", " + itemCode + " );";

            return SQL;
        }//end addLineItem


        public String updateInvoiceDate(String date, String invoiceNum)
        { //DATE TO BE IN FORMAT MM/DD/YYY
            String SQL = "UPDATE Invoices " +
                         "SET `InvoiceDate` = #" + date + "# " +
                         "WHERE `InvoiceNum` = " + invoiceNum + ";";

            return SQL;
        }//end updateInvoiceDate

        public String updateTotalCharge(String cost, String invoiceNum)
        {
            String SQL = "UPDATE Invoices " +
                         "SET `TotalCharge` = " + cost +
                         " WHERE `InvoiceNum` = " + invoiceNum + ";";

            return SQL;
        }//end updateTotalCharge


        /// <summary>
        /// Gets latest invoice entered.
        /// </summary>
        /// <returns></returns>
        public String latestInvoice()
        {
            String SQL = "select max(InvoiceNum) from invoices";
            return SQL;
        }//end latestInvoice

        // <summary>
        /// SQL query to get all the invoice #'s by date ***ADDED BY Martha
        /// </summary>
        /// <returns></returns>
        public string invoiceWithDate(String sDate)
        {
            string sSQL = "SELECT Invoices.InvoiceNum, Invoices.InvoiceDate "
                           + "FROM ItemDesc INNER JOIN (Invoices INNER JOIN LineItems ON Invoices.InvoiceNum = LineItems.InvoiceNum) ON ItemDesc.ItemCode = LineItems.ItemCode "
                           + "WHERE(((Invoices.InvoiceDate) =#" + sDate + "#))";
            return sSQL;
        }//end invoiceWithDate

        /// <summary>
        /// Method to Generate the SQL statement to select the
        /// date of an invoice
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public string SelectInvoiceDate2(string sDate)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sDate + "#";

            return sSQL;
        }//end SelectInvoiceDate2

        /// <summary>
        /// This SQL gets returns all data for each item in the Item table
        /// </summary>
        /// <returns>All item data</returns>
        public string getAllItems()
        {
            string sSQL = "SELECT ItemCode, Cost, ItemDesc FROM ItemDesc";

            return sSQL;
        }


        /// <summary>
        /// This SQL takes an Item_Code as a string.  It will search the Item table
        /// for the item that matches that code and delete that item from the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string deleteItem(string itemCode)
        {
            string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = " + itemCode;

            return sSQL;
        }

        /// <summary>
        /// This SQL updates a user selected item.
        /// Takes user input to update cost and description.
        /// Keeps old Item_Code and saves new cost and description to that Item_Code
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="cost"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public string updateItem(string itemCode, string cost, string description)
        {
            string sSQL = "UPDATE ItemDesc SET Cost = " + cost + ", ItemDesc = " + description + "  WHERE ItemCode = " + itemCode;

            return sSQL;
        }

        /// <summary>
        /// This SQL statement adds a new item to the database.
        /// Takes user input for cost and a description
        /// Item_Code is auto generated
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public string addItem(string cost, string description)
        {
            string sSQL = "INSERT INTO ItemDesc (Cost, ItemDesc) VALUES (" + cost + ", " + description + ")";

            return sSQL;
        }


    }//end class
}//end namespace
