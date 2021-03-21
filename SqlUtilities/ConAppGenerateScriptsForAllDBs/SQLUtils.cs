using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Agent;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ConAppGenerateScriptsForAllDBs
{
    class SQLUtils
    {
        public static List<string> GetDatabaseList(string strServer, string strUid, string strPwd, string strDb)
        {
            List<string> list = new List<string>();

            // Open connection to the database
            string conString = "server=" + strServer + ";uid=" + strUid + ";pwd=" + strPwd + "; database=" + strDb;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return list;
        }

        public static void GenerateDBScript(string strSQLServer, Server server)
        {
            //Server server = new Server();
            StreamWriter sw = new StreamWriter(strSQLServer + ".sql");

            Scripter scripter = new Scripter(server);
            Database myAdventureWorks = server.Databases[strSQLServer];

            Urn[] DatabaseURNs = new Urn[] { myAdventureWorks.Urn };
            StringCollection scriptCollection = scripter.Script(DatabaseURNs);

            /* With ScriptingOptions you can specify different scripting  
            * options, for example to include IF NOT EXISTS, DROP  
            * statements, output location etc*/
            ScriptingOptions scriptOptions = new ScriptingOptions();
            scriptOptions.ScriptDrops = true;
            scriptOptions.IncludeIfNotExists = true;
            foreach (Table myTable in myAdventureWorks.Tables)
            {
                /* Generating IF EXISTS and DROP command for tables */
                StringCollection tableScripts = myTable.Script(scriptOptions);
                foreach (string script in tableScripts)
                    //Console.WriteLine(script);
                    sw.WriteLine(script);

                /* Generating CREATE TABLE command */
                tableScripts = myTable.Script();
                foreach (string script in tableScripts)
                    //Console.WriteLine(script);
                    sw.WriteLine(script);
            }
            sw.Close();
        }

        public static void GenerateJobScript(Server srv3)
        {
            StringBuilder sb = new StringBuilder();
            var jv = srv3.JobServer;
            try
            {
                foreach (Job jx in jv.Jobs)
                {
                    StringCollection coll = jx.Script();
                    foreach (string str in coll)
                    {
                        //sb.Append(str);
                        //sb.Append(Environment.NewLine);
                        StreamWriter sw = new StreamWriter(jx.Name + "Job.sql");
                        sw.WriteLine(str);
                        sw.Close();
                    }
                    //var fs = File.CreateText(fileLocation);
                    //fs.Write(sb.ToString());
                    //fs.Close();
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
