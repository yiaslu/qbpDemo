using DBHelp;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        dbtest test = new dbtest();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test.Query.Where(DBWhereType.AND, TheSysTableInfo.QueryItem.tableid, DBTermType.Equal, 1);
            test.Query.Where(DBWhereType.AND, TheSysTableInfo.QueryItem.tableid, DBTermType.Equal, 2);

            xxx.Content = test.Query.SelectList("").Count;
        }
    }
    public class dbtest : PublicClass.PublicClass
    {
        BaseQuery<TheSysTableInfo> query = null;
        public dbtest()
        {
            SetConfig("license.crconfig");
            query = ConfigClass<TheSysTableInfo>.GetQuery();
            exec = ConfigClass<TheSysTableInfo>.GetExecute();
        }

        public BaseQuery<TheSysTableInfo> Query
        {
            get
            {
                return query;
            }
        }
        IExecute exec = null;

        public IExecute Exec
        {
            get
            {
                return exec;
            }
        }

    }
}
