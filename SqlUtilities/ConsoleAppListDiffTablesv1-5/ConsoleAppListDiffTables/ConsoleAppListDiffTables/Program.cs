using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleAppListDiffTables
{
  class Program
  {

    static void WriteColToFile(string strFile, List<string> ar)
    {
      try
      {
        //Pass the file path and file name to the StreamWriter constructor
        StreamWriter sr = new StreamWriter(strFile);

        //Write the first line of text
        foreach (string cols in ar)
        {
          sr.WriteLine(cols);
        }
        sr.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }
    }

    static List<string> Diff(List<string> ar, List<string> ar1)
    {
      List<string> arTemp = new List<string>();
      bool blnFound;
      foreach (string cols in ar)
      {
        blnFound = false;
        foreach (string cols1 in ar1)
        {
          if (cols.Equals(cols1))
          {
            blnFound = true;
            break;
          }
          
        }
        if (blnFound == false)
        {
          arTemp.Add(cols);
        }
      }
      return arTemp;
    }

    static void Main(string[] args)
    {
      List<string> ar = new List<string>();
      List<string> ar1 = new List<string>();
      List<string> arDiff = new List<string>();

      SqlDataReader drd;

      string strSchema = args[4];
      string strTable = args[5];
      SQLUtils objSU = new SQLUtils();
      ConnParameters objConn = new ConnParameters();

      objConn.strServerName = args[0];
      objConn.strDatabase = args[1];

      objSU.OpenConnectionCol(objConn);

      drd = objSU.GetAllColumns(strSchema, strTable);

      while (drd.Read())
      {
        //Console.WriteLine("{0}", drd.GetString(0));
        ar.Add(drd.GetString(0));
      }

      WriteColToFile(args[4] + args[1] + ".txt", ar);

      objSU.CloseconnCol();
      
      objConn.strServerName = args[2];
      objConn.strDatabase = args[3];

      objSU.OpenConnectionCol(objConn);
      drd = objSU.GetAllColumns(strSchema, strTable);

      while (drd.Read())
      {
        //Console.WriteLine("{0}", drd.GetString(0));
        ar1.Add(drd.GetString(0));
      }

      WriteColToFile(args[4] + "Svr2.txt", ar1);

      arDiff = Diff(ar, ar1);
      WriteColToFile("Diff" + args[4] + args[1] + ".txt", arDiff);

      arDiff = Diff(ar1, ar);
      WriteColToFile("Diff" + args[4] + "Svr2.txt", arDiff);

    }


  }
}
