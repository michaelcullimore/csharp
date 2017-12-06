using FinalProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        ItemDao itemDao;
        clsDataAccess db = new clsDataAccess();
        clsSQL sql = new clsSQL();
        List<Item> itemList = new List<Item>();
        Item currentItem;
        MessageBoxResult result;

        public EditWindow()
        {
            InitializeComponent();
            itemDao = new ItemDao(db);
            populateItemList();
        }

        /// <summary>
        /// Updates item in item list
        /// Keeps previous code for item selected
        /// Gets cost from user input in updateCostTextBox
        /// Gets description from user input in updateDescriptionTextBox
        /// Updates database with updated item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (evaluateUserInput(updateCostTextBox.Text, updateDescriptionTextBox.Text))
            {
                int index = itemList.IndexOf(currentItem);
                string cost = updateCostTextBox.Text.Trim('$');
                string desc = "\'" + updateDescriptionTextBox.Text + "\'";
                string code = itemList[index].Item_Code;
                currentItem.Item_Code = code;
                currentItem.Item_Description = desc.Trim('\'');
                currentItem.Item_Cost = "$" + cost;
                itemDao.updateItem(sql.updateItem(code, cost, desc));
                itemsDataGrid.ItemsSource = itemList;
                itemsDataGrid.Items.Refresh();
            }
        }

        /// <summary>
        /// Adds a new item to item list
        /// Automatically creates a code for the new item
        /// Gets cost from user input in addCostTextBox
        /// Gets description from user input in addDescriptionTextBox
        /// Updates database with new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (evaluateUserInput(addCostTextBox.Text, addDescriptionTextBox.Text))
            {
                string cost = addCostTextBox.Text.Trim('$');
                string desc = "\'" + addDescriptionTextBox.Text + "\'";
                itemDao.addItem(sql.addItem(cost, desc));
                itemList.Add(new Item((itemList.Count + 1).ToString(), addDescriptionTextBox.Text, "$" + cost));
                itemsDataGrid.ItemsSource = itemList;
                itemsDataGrid.Items.Refresh();
            }
        }

        /// <summary>
        /// Deletes item from item list
        /// Deletes selected item in itemsDataGrid
        /// Updates database, removing deleted item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            itemDao.addItem(sql.deleteItem(currentItem.Item_Code));
            itemList.Remove(currentItem);

            // Refresh DataGrid to reflect changes
            itemsDataGrid.ItemsSource = itemList;
            itemsDataGrid.Items.Refresh();
        }

        /// <summary>
        /// Brings user back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close this window, open main window
            this.Close();
        }

        /// <summary>
        /// Populates the DataGrid that holds all the items in inventory.
        /// Calls a sql method to return all items and returns a dataset of all items
        /// </summary>
        public void populateItemList()
        {
            try
            {
                DataSet ds = itemDao.getAllItems(sql.getAllItems());
                int i = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Item tempItem = new Item();
                    tempItem.Item_Code = dr["ItemCode"].ToString();
                    tempItem.Item_Description = dr["ItemDesc"].ToString();
                    tempItem.Item_Cost = "$" + dr["Cost"].ToString();
                    itemList.Add(tempItem);
                    i++;
                }
                itemsDataGrid.ItemsSource = itemList;
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// Method called when a cell is selected in the DataGrid that contains all items.
        /// Enables update and delete buttons.
        /// Fills in update text boxes with item cost and description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemsDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            UpdateButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            if (itemsDataGrid.CurrentItem == null)
            { }
            else
            {
                int rowIndex = itemsDataGrid.Items.IndexOf(itemsDataGrid.CurrentItem);
                updateDescriptionTextBox.Text = itemList[rowIndex].Item_Description.Trim('\'');
                updateCostTextBox.Text = itemList[rowIndex].Item_Cost;
                currentItem = itemList[rowIndex];
            }
        }

        /// <summary>
        /// Method used to determine bad input on submitting a new item or updating an item
        /// </summary>
        /// <param name="costInput"></param>
        /// <param name="descInput"></param>
        /// <returns></returns>
        private Boolean evaluateUserInput(string costInput, string descInput)
        {
            int num = -1;
            if (costInput == "")
            {
                result = MessageBox.Show("Cost cannot be left blank", "Empty cost", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!int.TryParse(costInput.Trim('$'), out num))
            {
                result = MessageBox.Show("Cost must be a positive number", "Not a cost", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (descInput == "")
            {
                result = MessageBox.Show("Description cannot be left blank", "No Description", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}

