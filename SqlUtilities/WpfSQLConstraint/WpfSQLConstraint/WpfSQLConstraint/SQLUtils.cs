using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WpfSQLConstraint
{
  class SQLUtils
  {
    SqlConnection conn;
    SqlDataAdapter da;
    SqlCommandBuilder cb;
    DataSet ds;
    DataTable tblCat;
    DataRow dr;
    int RowCtr;
    
    public void OpenConnection(ConnParameters objConn)
    {
      string strconn = "Data Source=" + objConn.strServerName +
                       ";Initial Catalog= " + objConn.strDatabase + 
                       ";Integrated Security=True";
     
      conn = new SqlConnection(strconn);
     
      conn.Open();
     
    }

    public DataTable OpenDataSet()
    {
      String strSQL = "SELECT " +
                      "KCU1.CONSTRAINT_NAME AS 'FK_CONSTRAINT_NAME' " +
                      ", KCU1.TABLE_NAME AS 'FK_TABLE_NAME' " +
                      ", KCU1.COLUMN_NAME AS 'FK_COLUMN_NAME' " +
                      ", KCU1.ORDINAL_POSITION AS 'FK_ORDINAL_POSITION' " +
                      ", KCU2.CONSTRAINT_NAME AS 'UQ_CONSTRAINT_NAME' " +
                      ", KCU2.TABLE_NAME AS 'UQ_TABLE_NAME' " +
                      ", KCU2.COLUMN_NAME AS 'UQ_COLUMN_NAME' " +
                      ", KCU2.ORDINAL_POSITION AS 'UQ_ORDINAL_POSITION' " +
                      "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC " +
                      "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 " +
                      "ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG " +
                      "AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA " +
                      "AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " +
                      "JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 " +
                      "ON KCU2.CONSTRAINT_CATALOG = " +
                      "RC.UNIQUE_CONSTRAINT_CATALOG " +
                      "AND KCU2.CONSTRAINT_SCHEMA = " +
                      "RC.UNIQUE_CONSTRAINT_SCHEMA " +
                      "AND KCU2.CONSTRAINT_NAME = " +
                      "RC.UNIQUE_CONSTRAINT_NAME " +
                      "AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION";

      da = new SqlDataAdapter(strSQL, conn);
      //  This line is a must and must be after the da
      cb = new SqlCommandBuilder(da);

      ds = new DataSet("DOC");
      da.Fill(ds, "DOC");
      tblCat = ds.Tables["DOC"];

      if (tblCat.Rows.Count > 0)
      {
        dr = tblCat.Rows[RowCtr];
        //    FillData()
      }
      return tblCat;
    }    
  }
}
