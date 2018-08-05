using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSQLConstraint
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      ConnParameters objconn = new ConnParameters(txtSN.Text, txtDB.Text);
      SQLUtils objsql = new SQLUtils();
      objsql.OpenConnection(objconn);
      //Bind Datacontext to DataTable
      this.DataContext = objsql.OpenDataSet();
      
    }
  }
}
