using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppSQLUntils
{
    class SQLAccumulate
    {
      string strSQL;

      public SQLAccumulate()
      {
        strSQL = "";
      }

      public string SQLStatements
      {
        get {return strSQL;}
        set { strSQL = value; }
      }

      public void Accumulate(string strTempSQL)
      {
        strSQL += strTempSQL;
      }
    }
}
