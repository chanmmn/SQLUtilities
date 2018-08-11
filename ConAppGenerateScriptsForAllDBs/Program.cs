using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;

namespace ConAppGenerateScriptsForAllDBs
{
    class Program
    {
        static void Main(string[] args)
        {
            string strServer = "172.18.123.105";
            string strUid = "sa";
            string strPwd = "password";
            string strDb = "Dashboard15";

            string strconn = "Data Source=" + strServer +
                 ";Initial Catalog= " + strDb +
                  ";User ID=" + strUid +
                  ";Password=" + strPwd;
            //SqlConnection conn = new SqlConnection(strconn);
            ServerConnection conn = new ServerConnection(strServer, strUid, strPwd);

            Server server = new Server(conn);

            //Create Database SCriptt -----------------
            //List<string> lstDatabase = SQLUtils.GetDatabaseList(strServer, strUid, strPwd, strDb);
            //foreach (string str in lstDatabase)
            //{
            //    SQLUtils.GenerateDBScript(str, server);
            //}
            //-----------------------------------------------

            //Create Job SCript
            SQLUtils.GenerateJobScript(server);
            //------------------------------------------------
        }
    }
}
