using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ConAppFindFieldname
{
  class Program
  {
    static void Main(string[] args)
    {
      SqlDataReader drd;
      SqlDataReader drdCol;

      int intCount = 0;

      SQLUtils objSU = new SQLUtils();
      ConnParameters objConn = new ConnParameters();

      objConn.strServerName = args[0];
      objConn.strDatabase = args[1];

      objSU.OpenConnection(objConn);

      drd = objSU.GetAllTables();

      while (drd.Read())
      {
        Console.WriteLine("Schema: {0}  Table: {1} ", drd.GetString(0), drd.GetString(1));
        objSU.OpenConnectionCol(objConn);

        drdCol = objSU.GetAllColumns(drd.GetString(0), drd.GetString(1));

        while (drdCol.Read())
        {
          Console.WriteLine("Column: {0} ", drdCol.GetString(0));

          if (drdCol.GetString(0) == args[2])
          {
            Console.WriteLine(intCount + 1);
            intCount++;
            Console.WriteLine("Type any press and Press Enter to continue");
            Console.ReadLine();
          }

        }
      }
      objSU.CloseconnCol();
    }
  }
}
