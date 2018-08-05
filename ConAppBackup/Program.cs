using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAppBackup
{
    class Program
    {

        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader dr;
        static void Main(string[] args)
        {
            string strDBServer = "localhost";
            string strDB = "northwnd";
            string strBackupFile = @"c:\database\backup\northwnd.bak";
            Createconnection(strDBServer);
            Backup(strBackupFile, strDB);
        }

        public static void Createconnection(string strDBServer)
        {
            con = new SqlConnection("Data Source=" + (strDBServer) + ";Database=Master;data source=.; uid=sa; pwd=Pa$$w0rd");
            con.Open();
        }

        public static void Backup(string strBackupFile, string strDBName)
        {
            string s = null;
            s = strBackupFile;
            query("Backup database " + strDBName + " to disk='" + s + "'");
        }

        public static void query(string que)
        {
            // ERROR: Not supported in C#: OnErrorStatement
            cmd = new SqlCommand(que, con);
            cmd.ExecuteNonQuery();
        }
    }
}
