using DBHelp.DataExecute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelp
{
    public class ConfigClass<T> where T : PublicClass.BaseMODEL, new()
    {
        readonly static string str = PublicClass.PublicClass.BOX.ExecType;
        public static BaseQuery<T> GetQuery()
        {
            BaseQuery<T> query = null;
            switch (str)
            {
                case "MDB": query = new DBQuery<T>(); break;
                case "JSON": query = new JFQuery<T>(); break;
                case "EF": query = new EFQuery<T>(); break;
            }
            return query;
        }
        public static IExecute GetExecute()
        {
            IExecute exec = null;
            switch (str)
            {
                case "MDB": exec = new DBExecute(); break;
                case "JSON": exec = new JFExecute(); break;
                case "EF": exec = new EFExecute(); break;
            }
            return exec;
        }
    }
}
