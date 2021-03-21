using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleAppListDiffTables
{
  class SQLUtils
  {
    SqlConnection connCol;
    
    public void OpenConnectionCol(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase +
                       ";Integrated Security=True";

      connCol = new SqlConnection(strconn);

      connCol.Open();
    }

    public SqlDataReader GetAllColumns(string strSchema, string strTable)
    {
      
      string strSQL = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + strSchema + "' AND TABLE_NAME= '[" +strTable + "]'";
      SqlDataReader drdCol;
       
      SqlCommand cmd = new SqlCommand(strSQL, connCol);

      drdCol = cmd.ExecuteReader();
      
      return drdCol;
    }

    public void CloseconnCol()
    {
      connCol.Close();
    }
  }
}
