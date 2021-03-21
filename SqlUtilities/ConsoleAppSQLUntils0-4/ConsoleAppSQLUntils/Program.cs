using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleAppSQLUntils
{
  class Program
  {
    // WINXPSQL FFMSDW0-2 Tricia
    static void Main(string[] args)
    {
      List<string> lstStr = new List<string>();
      SqlDataReader drd;
      SqlDataReader drdCol;
      SqlDataReader drdText;
      bool connTextOpen = false;
      int intCount = 0;

      StreamWriter sw = new StreamWriter("ListTables.txt");

      SQLUtils objSU = new SQLUtils();
      ConnParameters objConn = new ConnParameters();

      objConn.strServerName = args[0];
      objConn.strDatabase = args[1];
      
      //objSU.OpenConnection(objConn);
      objSU.OpenConnectionsql("devsa", "1qaz", objConn);

      drd = objSU.GetAllTables();
      
      while (drd.Read())
      {
        //List schema name and tables name
        //Console.WriteLine("Schema: {0}  Table: {1} ", drd.GetString(0), drd.GetString(1));
        sw.WriteLine("Schema: {0}  Table: {1} ", drd.GetString(0), drd.GetString(1));
        //objSU.OpenConnectionCol(objConn);
        objSU.OpenConnectionColsql("devsa", "1qaz", objConn);
        
        drdCol = objSU.GetAllColumns(drd.GetString(0),drd.GetString(1));
        
        while (drdCol.Read())
        {
          //Console.WriteLine("Column: {0} ", drdCol.GetString(0));
          sw.WriteLine("Column: {0} ", drdCol.GetString(0));
          objSU.OpenConnectionTextsql("devsa", "1qaz", objConn);

          drdText = objSU.FindContain(args[2],drd.GetString(0),drd.GetString(1),drdCol.GetString(0));
          //break if contain found
          if (drdText.Read())
          {
            //Console.WriteLine(intCount + 1);
            sw.WriteLine(intCount + 1);
            intCount++;
            Console.WriteLine("Type any press and Press Enter to continue");
            Console.ReadLine();
          }
          connTextOpen = true;
        }

        intCount = 0;
        if (connTextOpen == true)
        {
          objSU.CloseconnText();
          connTextOpen = false;
        }
        objSU.CloseconnCol();
       }
      //Close file stream 
      sw.Close();
    }
  }
}
