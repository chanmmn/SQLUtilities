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
            SqlDataReader drdCol;
            SqlDataReader drdText;
            bool connTextOpen = false;
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

                    //string strColName = drdCol.GetString(0);
                    //string strTypeName = drdCol.GetDataTypeName(0);
                    //Type type = drdCol.GetFieldType(0);
                    //string strDataType = type.FullName;
                   
                    objSU.OpenConnectionText(objConn);

                    //drdText = objSU.FindContain(int.Parse(args[2]), drd.GetString(0), drd.GetString(1), drdCol.GetString(0));
                    drdText = objSU.FindContain(args[2].ToString(), drd.GetString(0), drd.GetString(1), drdCol.GetString(0), objConn);
                    Console.WriteLine(intCount + 1);

                    if (drdText.Read())
                    {
                        Console.WriteLine("Found {0}", args[2].ToString());
                    }

                    intCount++;
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
        }
    }
}
