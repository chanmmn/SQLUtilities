using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
namespace SQLScriptGenerationProgrammatically
{
    class Program
    {
        static void Main(string[] args)
        {
            Server myServer = new Server(@"localhost");
            try
            {
                //Using windows authentication
                myServer.ConnectionContext.LoginSecure = true;
                myServer.ConnectionContext.Connect();
                //GenerateDBScript(myServer);
                GenerateTableScript(myServer);
                //GenerateTableScriptWithDependencies(myServer);
                //GenerateTableScriptWithIndexes(myServer);
                //GenerateScriptWithoutCreatingObjectOnServer(myServer);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (myServer.ConnectionContext.IsOpen)
                    myServer.ConnectionContext.Disconnect();
                Console.WriteLine("Press any key to terminate....");
                Console.ReadKey();
            }
        }
        private static void GenerateDBScript(Server myServer)
        {
            Scripter scripter = new Scripter(myServer);
            Database myAdventureWorks = myServer.Databases["AdventureWorks"];
            StringCollection scriptCollection = scripter.Script(new Urn[] { myAdventureWorks.Urn });
            foreach (string script in scriptCollection)
                Console.WriteLine(script);
        }
        private static void GenerateTableScript(Server myServer)
        {
            Scripter scripter = new Scripter(myServer);
            //Database myAdventureWorks = myServer.Databases["AdventureWorks"];
            Database myAdventureWorks = myServer.Databases["poc"];
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
                    Console.WriteLine(script);
                /* Generating CREATE TABLE command */
                tableScripts = myTable.Script();
                foreach (string script in tableScripts)
                    Console.WriteLine(script);
            }
        }
        private static void GenerateTableScriptWithDependencies(Server myServer)
        {
            Scripter scripter = new Scripter(myServer);
            Database myAdventureWorks = myServer.Databases["AdventureWorks"];
            Table myTable = myAdventureWorks.Tables["EmployeeAddress", "HumanResources"];
            /* Generate Scripts of table along with for all
             * objects on which this table depends on */
            ScriptingOptions scriptOptionsForDependendencies = new ScriptingOptions();
            scriptOptionsForDependendencies.WithDependencies = true;
            /* DriAll will include all DRI objects in the generated script. */
            scriptOptionsForDependendencies.DriAll = true;
            /* You can optionally can choose each DRI object separately as given below */
            //scriptOptionsForDependendencies.DriAllConstraints = true;
            //scriptOptionsForDependendencies.DriAllKeys = true;
            //scriptOptionsForDependendencies.DriChecks = true;
            //scriptOptionsForDependendencies.DriClustered = true;
            //scriptOptionsForDependendencies.DriDefaults = true;
            //scriptOptionsForDependendencies.DriForeignKeys = true;
            //scriptOptionsForDependendencies.DriIndexes = true;
            //scriptOptionsForDependendencies.DriNonClustered = true;
            //scriptOptionsForDependendencies.DriPrimaryKey = true;
            //scriptOptionsForDependendencies.DriUniqueKeys = true;
            /* If you can use FileName to output generated script in a file 
             * Note : You need to have access on the specified location*/
            scriptOptionsForDependendencies.FileName = @"D:\TableScriptWithDependencies.sql";
            StringCollection tableScripts = myTable.Script(scriptOptionsForDependendencies);
            foreach (string script in tableScripts)
                Console.WriteLine(script);
        }
        private static void GenerateTableScriptWithIndexes(Server myServer)
        {
            Scripter scripter = new Scripter(myServer);
            Database myAdventureWorks = myServer.Databases["AdventureWorks"];
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
                    Console.WriteLine(script);
                /* Generating CREATE TABLE command */
                tableScripts = myTable.Script();
                foreach (string script in tableScripts)
                    Console.WriteLine(script);
                IndexCollection indexCol = myTable.Indexes;
                foreach (Index myIndex in myTable.Indexes)
                {
                    /* Generating IF EXISTS and DROP command for table indexes */
                    StringCollection indexScripts = myIndex.Script(scriptOptions);
                    foreach (string script in indexScripts)
                        Console.WriteLine(script);
                    /* Generating CREATE INDEX command for table indexes */
                    indexScripts = myIndex.Script();
                    foreach (string script in indexScripts)
                        Console.WriteLine(script);
                }
            }
        }
        private static void GenerateScriptWithoutCreatingObjectOnServer(Server myServer)
        {
            /* Drop the database if it exists */
            if (myServer.Databases["MyNewDatabase"] != null)
                myServer.Databases["MyNewDatabase"].Drop();
            /* Create database called, "MyNewDatabase" */
            Database myDatabase = new Database(myServer, "MyNewDatabase");
            /* Output the database script on the console */
            StringCollection DBScripts = myDatabase.Script();
            foreach (string script in DBScripts)
                Console.WriteLine(script);
            /* Create a table instance */
            Table myEmpTable = new Table(myDatabase, "MyEmpTable");
            /* Add [EmpID] column to created table instance */
            Column empID = new Column(myEmpTable, "EmpID", DataType.Int);
            empID.Identity = true;
            myEmpTable.Columns.Add(empID);
            /* Add another column [EmpName] to created table instance */
            Column empName = new Column(myEmpTable, "EmpName", DataType.VarChar(200));
            empName.Nullable = true;
            myEmpTable.Columns.Add(empName);
            /* Add third column [DOJ] to created table instance with default constraint */
            Column DOJ = new Column(myEmpTable, "DOJ", DataType.DateTime);
            DOJ.AddDefaultConstraint(); // you can specify constraint name here as well
            DOJ.DefaultConstraint.Text = "GETDATE()";
            myEmpTable.Columns.Add(DOJ);
            /* Add primary key index to the table */
            Index primaryKeyIndex = new Index(myEmpTable, "PK_MyEmpTable");
            //primaryKeyIndex.IndexKeyType = IndexKeyType.DriPrimaryKey;
            primaryKeyIndex.IndexedColumns.Add(new IndexedColumn(primaryKeyIndex, "EmpID"));
            myEmpTable.Indexes.Add(primaryKeyIndex);
            /* Output the table script on the console */
            StringCollection TableScripts = myEmpTable.Script();
            foreach (string script in TableScripts)
                Console.WriteLine(script);
            /* If you want to create objects on the server you need call 
             * create method or else objects will not be created on the server */
            myDatabase.Create();
            myEmpTable.Create();
        }
    }
}