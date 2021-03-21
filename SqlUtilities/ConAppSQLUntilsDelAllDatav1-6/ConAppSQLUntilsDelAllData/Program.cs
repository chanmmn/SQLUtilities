using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleAppSQLUntils
{
  class Program
  {
    // WINXPSQL FFMSDW0-2 Tricia
    static void Main(string[] args)
    {
      SqlDataReader drd;
      
      SQLUtils objSU = new SQLUtils();
      SQLAccumulate objSA = new SQLAccumulate();
      ConnParameters objConn = new ConnParameters();
      string strTempSQL;

      objConn.strServerName = args[0];
      objConn.strDatabase = args[1];
      
      objSU.OpenConnection(objConn);

      drd = objSU.GetAllForeignKeys();

      while (drd.Read())
      {
        Console.WriteLine("Schema: {0}  Table: {1} Constraint: {2} " , drd.GetString(0), drd.GetString(1), drd.GetString(2));

        strTempSQL = "ALTER TABLE " + drd.GetString(0) + "." + drd.GetString(1) + " NOCHECK CONSTRAINT " + drd.GetString(2) + ";";

        objSA.Accumulate(strTempSQL);
      }

      drd.Close();
      drd = objSU.GetAllTables();

      while (drd.Read())
      {
        Console.WriteLine("--------------- Table name ------------------------");
        Console.WriteLine("Schema: {0}  Table: {1} ", drd.GetString(0), drd.GetString(1));

        strTempSQL = "Delete FROM " + drd.GetString(0) + "." + drd.GetString(1) + ";";

        objSA.Accumulate(strTempSQL);
      }
      drd.Close();

      drd = objSU.GetAllForeignKeys();

      while (drd.Read())
      {
        Console.WriteLine("Schema: {0}  Table: {1} Constraint: {2} ", drd.GetString(0), drd.GetString(1), drd.GetString(2));

        strTempSQL = "ALTER TABLE " + drd.GetString(0) + "." + drd.GetString(1) + " CHECK CONSTRAINT " + drd.GetString(2) + ";";

        objSA.Accumulate(strTempSQL);
      }
      drd.Close();

      objSU.OpenConnectionDel(objConn);
      objSU.DeleteAllData(objSA);
      objSU.Closeconn();
    }
  }
}
