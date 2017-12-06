using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// This class uses the DataAccess class to make calls to the database and return datasets belonging to the Item table
    /// </summary>
    class ItemDao
    {
        clsDataAccess db;
        int iRet;
    
        /// <summary>
        /// Constructer requires a DataAccess object
        /// </summary>
        /// <param name="db"></param>
        public ItemDao(clsDataAccess db)
        {
            this.db = db;
            iRet = 0;
        }

        /// <summary>
        /// This method executes the SQL statement that returns all items in a data set.
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public DataSet getAllItems(String sqlStatement)
        {
            try
            {
                return db.ExecuteSQLStatement(sqlStatement, ref iRet);
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This method executes the SQL statement that updates a particular item in the database
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public int updateItem(String sqlStatement)
        {
            try
            {
                return db.ExecuteNonQuery(sqlStatement);
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }

        /// <summary>
        /// This method executes the SQL statement that updates a particular item in the database
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <returns></returns>
        public int addItem(String sqlStatement)
        {
            try
            {
                return db.ExecuteNonQuery(sqlStatement);
            }
            catch (Exception e)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + e.Message);
            }
        }
    }
}
