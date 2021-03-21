using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAppReconstructDBusingSql
{
    class SQLUtils
    {
        public static void CreateeDB(string strConn, string strDBPath, string strDBName)
        {
            String str;
            SqlConnection myConn = new SqlConnection(strConn);

            str = "CREATE DATABASE " + strDBName + " ON PRIMARY " +
                "(NAME = " + strDBName + "_Data, " +
                "FILENAME = '"+ strDBPath + "\\" + strDBName + ".mdf', " +
                "SIZE = 10MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = " + strDBName + "_Log, " +
                "FILENAME = '" + strDBPath + "\\" + strDBName + ".ldf', " +
                "SIZE = 4MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";

            myConn.Open();
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {

                myCommand.ExecuteNonQuery();
                //MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.Write(ex.ToString());
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public static void CreateTable(string strDBName, string strConn)
        {
            string strSql = ReadSqlToString(strDBName);

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static string ReadSqlToString(string strDBName)
        {
            string temp = "";

            string path = strDBName;
            temp = File.ReadAllText(path);

            return temp;
        }
    }
}
