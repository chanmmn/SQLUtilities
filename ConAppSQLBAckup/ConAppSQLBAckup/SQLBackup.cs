using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConAppSQLBAckup
{
  class SQLBackup
  {
    SqlConnection conn;

    public void OpenConnection(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase +
                       ";Integrated Security=True";

      conn = new SqlConnection(strconn);

      conn.Open();
    }

    public void BackupDatabase(ConnParameters objConn, string strLocation)
    {
      string strSQL = "BACKUP DATABASE " + objConn.strDatabase + " TO DISK='" + strLocation + "'";
      SqlCommand cmd = new SqlCommand(strSQL, conn);

      cmd.ExecuteNonQuery();      
    }
  }
}
