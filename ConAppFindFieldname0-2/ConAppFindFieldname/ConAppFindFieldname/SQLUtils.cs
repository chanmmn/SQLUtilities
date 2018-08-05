using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConAppFindFieldname
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
                       ";Integrated Security=True;Connection Timeout=720";

      conn = new SqlConnection(strconn);

      conn.Open();
    }

    public void OpenConnectionCol(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase +
                       ";Integrated Security=True;Connection Timeout=720";

      connCol = new SqlConnection(strconn);

      connCol.Open();

    }

        public SqlDataReader GetAllTables()
        {
            string strSQL = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> 'VIEW'";
            SqlDataReader drd;
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandTimeout = 720;

            drd = cmd.ExecuteReader();
            conn.Close();
            return drd;
        }

        public SqlDataReader GetAllColumns(string strSchema, string strTable)
        {

            string strSQL = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + strSchema + "' AND TABLE_NAME= '" + strTable + "' AND DATA_TYPE LIKE '%c%'";
            SqlDataReader drdCol;

            SqlCommand cmd = new SqlCommand(strSQL, connCol);
            cmd.CommandTimeout = 720;

            drdCol = cmd.ExecuteReader();
            connCol.Close();
            return drdCol;
        }

    public void CloseconnCol()
    {
      connCol.Close();
    }
  }
}
