using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfSQLConstraint
{
  class ConnParameters
  {
    public string strServerName;
    public string strDatabase;

    public ConnParameters()
    {
    }

    public ConnParameters(string strSN, string strDB)
    {
      strServerName = strSN;
      strDatabase = strDB;
    }
  }
}
