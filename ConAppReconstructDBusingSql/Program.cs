using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAppReconstructDBusingSql
{
    class Program
    {
        static void Main(string[] args)
        {
            string strServer = "172.18.123.74";
            string strUid = "sa";
            string strPwd = "password";
            string strDb = "Dashboard15";

            string strconn = "Data Source=" + strServer +
                 ";Initial Catalog= " + strDb +
                  ";User ID=" + strUid +
                  ";Password=" + strPwd;

            //SQLUtils.CreateeDB(strconn, "c:\\database", "ExpIncPm");

            strServer = "172.18.123.74";
            strUid = "sa";
            strPwd = "password";
            strDb = "ExpIncPm";

            strconn = "Data Source=" + strServer +
                 ";Initial Catalog= " + strDb +
                  ";User ID=" + strUid +
                  ";Password=" + strPwd;

            SQLUtils.CreateTable("ExpIncPm.sql", strconn);
        }
    }
}
