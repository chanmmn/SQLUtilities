using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConAppSQLBAckup
{
  class Program
  {
    static void Main(string[] args)
    {
      SQLBackup objSB = new SQLBackup();
      ConnParameters conn = new ConnParameters(args[0], args[1]);
      objSB.OpenConnection(conn);
      objSB.BackupDatabase(conn, args[2]);
      Console.WriteLine("Backup completed!!!");
    }
  }
}
