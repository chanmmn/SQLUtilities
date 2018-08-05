using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleAppSQLUntils
{
    class SQLUtils
    {
        SqlConnection conn;
        SqlConnection connCol;
        SqlConnection connText;

        public void OpenConnection(ConnParameters objConn)
        {
            string strconn = "Data Source=" + objConn.strServerName +
                             ";Initial Catalog= " + objConn.strDatabase +
                             ";Integrated Security=True";

            conn = new SqlConnection(strconn);

            conn.Open();

        }

        public void OpenConnectionCol(ConnParameters objConn)
        {
            string strconn = "Data Source=" + objConn.strServerName +
                             ";Initial Catalog= " + objConn.strDatabase +
                             ";Integrated Security=True";

            connCol = new SqlConnection(strconn);

            connCol.Open();

        }

        public void OpenConnectionText(ConnParameters objConn)
        {
            string strconn = "Data Source=" + objConn.strServerName +
                             ";Initial Catalog= " + objConn.strDatabase +
                             ";Integrated Security=True";

            connText = new SqlConnection(strconn);

            connText.Open();

        }

        public SqlDataReader GetAllTables()
        {
            string strSQL = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
            SqlDataReader drd;
            SqlCommand cmd = new SqlCommand(strSQL, conn);

            drd = cmd.ExecuteReader();
            conn.Close();
            return drd;
        }

        public SqlDataReader GetAllColumns(string strSchema, string strTable)
        {

            string strSQL = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + strSchema + "' AND TABLE_NAME= '" + strTable + "' AND DATA_TYPE LIKE '%int%'";
            SqlDataReader drdCol;

            SqlCommand cmd = new SqlCommand(strSQL, connCol);

            drdCol = cmd.ExecuteReader();

            return drdCol;
        }

        public SqlDataReader FindContain(int intFind, string strSchema, string strTable, string strCol)
        {
            string strSQL = "SELECT * FROM " + strSchema + "." + strTable + " WHERE [" + strCol + "] =" + intFind;
            SqlDataReader drdText;

            SqlCommand cmd = new SqlCommand(strSQL, connText);

            drdText = cmd.ExecuteReader();
            cmd.CommandTimeout = 240;
            return drdText;
        }

        public SqlDataReader FindContain(string strFind, string strSchema, string strTable, string strCol, ConnParameters objConn)
        {
            //For this method
            SqlConnection connThis;
            string strconn = "Data Source=" + objConn.strServerName +
                 ";Initial Catalog= " + objConn.strDatabase +
                 ";Integrated Security=True";

            connThis = new SqlConnection(strconn);

            connThis.Open();

            //Todo
            string strPreSql = "SELECT * FROM " + strSchema + ".[" + strTable + "]";
            SqlDataReader dataReader;

            SqlCommand command = new SqlCommand(strPreSql, connText);
            dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                //string strColName = dataReader.GetString(0);
                string strTypeName = dataReader.GetDataTypeName(0);
                //Type type = drdText.GetFieldType(0);
                //string strDataType = type.FullName;
                if (strTypeName != "nvarchar")
                {
                    return dataReader;
                }
            }

            string strSQL = "SELECT * FROM " + strSchema + ".[" + strTable + "] WHERE [" + strCol + "] ='" + strFind + "'";
            SqlDataReader drdText;

            SqlCommand cmd = new SqlCommand(strSQL, connThis);

            drdText = cmd.ExecuteReader();
            cmd.CommandTimeout = 240;

            //if (drdText.Read())
            //{
            //    //string strColName = dataReader.GetString(0);
            //    string strTypeName = drdText.GetDataTypeName(0);
            //    //Type type = drdText.GetFieldType(0);
            //    //string strDataType = type.FullName;
            //    if (strTypeName != "nvarchar")
            //    {
            //        return null;
            //    }
            //}
            return drdText;
        }

        public void CloseconnCol()
        {
            connCol.Close();
        }

        public void CloseconnText()
        {
            connText.Close();
        }


    }
}
