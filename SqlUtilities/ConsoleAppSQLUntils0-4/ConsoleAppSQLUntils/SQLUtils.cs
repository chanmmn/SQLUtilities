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
                       ";Integrated Security=True;Connection Timeout=720";
     
      conn = new SqlConnection(strconn);
     
      conn.Open();
     
    }

    public void OpenConnectionsql(string strusername, string strpwd, ConnParameters objConn)
    {
        string strconn = "Data Source=" + objConn.strServerName +
                         ";Initial Catalog= " + objConn.strDatabase +
                         ";User ID=" + strusername +
                         ";Password=" + strpwd + 
                         ";Connection Timeout=720";

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

    public void OpenConnectionColsql(string strusername, string strpwd, ConnParameters objConn)
    {
        string strconn = "Data Source=" + objConn.strServerName +
                         ";Initial Catalog= " + objConn.strDatabase +
                         ";User ID=" + strusername +
                         ";Password=" + strpwd +
                         ";Connection Timeout=720";

        connCol = new SqlConnection(strconn);

        connCol.Open();

    }

    public void OpenConnectionText(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase +
                       ";Integrated Security=True;Connection Timeout=2880";

      connText = new SqlConnection(strconn);
      
      connText.Open();
      

    }

    public void OpenConnectionTextsql(string strusername, string strpwd, ConnParameters objConn)
    {
        string strconn = "Data Source=" + objConn.strServerName +
                         ";Initial Catalog= " + objConn.strDatabase +
                         ";User ID=" + strusername +
                         ";Password=" + strpwd + 
                         ";Connection Timeout=2880";

        connText = new SqlConnection(strconn);

        connText.Open();


    }

    public SqlDataReader GetAllTables()
    {
      string strSQL = "SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> 'VIEW'";
      SqlDataReader drd;
      SqlCommand cmd = new SqlCommand(strSQL,conn);
      cmd.CommandTimeout = 720;

      drd = cmd.ExecuteReader();
      return drd;
    }

    public SqlDataReader GetAllColumns(string strSchema, string strTable)
    {
      
      string strSQL = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '" + strSchema + "' AND TABLE_NAME= '" +strTable + "' AND DATA_TYPE LIKE '%c%'";
      SqlDataReader drdCol;
       
      SqlCommand cmd = new SqlCommand(strSQL, connCol);
      cmd.CommandTimeout = 720;
      
      drdCol = cmd.ExecuteReader();
      
      return drdCol;
    }

    public SqlDataReader FindContain(string strFind, string strSchema, string strTable, string strCol)
    {
      string strSQL = "SELECT * FROM " + strSchema + "." + strTable + " WHERE [" + strCol + "] LIKE '%" + strFind + "%'";
      SqlDataReader drdText;

      SqlCommand cmd = new SqlCommand(strSQL, connText);
      cmd.CommandTimeout = 720;

      drdText = cmd.ExecuteReader();

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
