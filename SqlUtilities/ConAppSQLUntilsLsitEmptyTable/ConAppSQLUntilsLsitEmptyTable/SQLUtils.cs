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
    SqlConnection connTop;
    
    public void OpenConnection(ConnParameters objConn)
    {
        string strconn = "Data Source=" + objConn.strServerName +
                         ";Initial Catalog= " + objConn.strDatabase +
                         ";Integrated Security=True";
     
      conn = new SqlConnection(strconn);
     
      conn.Open();
     
    }

    public void OpenConnectionTop(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase +
                       ";Integrated Security=True";

      connTop = new SqlConnection(strconn);

      connTop.Open();

    }

    public SqlDataReader GetAllTables()
    {
      string strSQL = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME NOT LIKE '%sys%' AND TABLE_TYPE <> 'VIEW'";
      SqlDataReader drd;
      SqlCommand cmd = new SqlCommand(strSQL,conn);
      
      drd = cmd.ExecuteReader();
      return drd;
    }

    public SqlDataReader GetAllForeignKeys()
    {
      string strSQL = "SELECT CONSTRAINT_SCHEMA, TABLE_NAME, CONSTRAINT_NAME  FROm INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'";
      SqlDataReader drd;

      SqlCommand cmd = new SqlCommand(strSQL, conn);

      drd = cmd.ExecuteReader();

      return drd;
    }

    public SqlDataReader CheckTopTenRows(string strTable)
    {
      SqlDataReader drd;
      string strSQL = "SELECT TOP 10 * FROM " + strTable;
      SqlCommand cmd = new SqlCommand(strSQL, connTop);

      drd = cmd.ExecuteReader();
      return drd;
    }    

    public void Closeconn()
    {
      conn.Close();
    }

    public void CloseconnTop()
    {
      connTop.Close();
    }
  }
}
