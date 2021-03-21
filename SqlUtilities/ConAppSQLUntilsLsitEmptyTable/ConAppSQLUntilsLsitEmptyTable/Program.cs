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
    static void WriteToText(List<string> strList)
    {
      

      // Create an instance of StreamWriter to write text to a file.
      // The using statement also closes the StreamWriter.
      using (StreamWriter sw = new StreamWriter("EmptyTables.txt"))
      {
        foreach (string s in strList)
        {
          // Add some text to the file.
          sw.WriteLine(s);
        }
      }
    }

    static void Main(string[] args)
    {
      SqlDataReader drd;
      List<string> lstString = new List<string>();
       
      SQLUtils objSU = new SQLUtils();
      
      ConnParameters objConn = new ConnParameters();

      objConn.strServerName = args[0];
      objConn.strDatabase = args[1];
      
      objSU.OpenConnection(objConn);
            
      drd = objSU.GetAllTables();

      while (drd.Read())
      {
        //Connection open and reopen
        objSU.OpenConnectionTop(objConn);

        //To take the return of the ten rows from any table
        SqlDataReader drdTopTen; 

        Console.WriteLine("--------------- Table name ------------------------");
        Console.WriteLine("Schema: {0}  Table: {1} ", drd.GetString(0), drd.GetString(1));

        drdTopTen = objSU.CheckTopTenRows(drd.GetString(1));

        //if the data reader is empty then add to array list 
        if (!(drdTopTen.Read()))
        {
          lstString.Add(drd.GetString(1));
        }
        //need to close the connection or else the second data reader will not read
        objSU.CloseconnTop();
      }
      drd.Close();

      WriteToText(lstString);
      
    }
  }
}
