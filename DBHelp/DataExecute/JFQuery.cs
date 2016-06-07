using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBHelp
{
    public class JFQuery<T> : BaseQuery<T> where T : PublicClass.BaseMODEL, new()
    {

        List<T> GetListModel(string str)
        {
            List<T> retList = new List<T>();

            return retList;
        }
        public override List<T> SelectList(string orderbystr)
        {
            List<T> retList = new List<T>();
            BaseMODEL itemObj = (BaseMODEL)Activator.CreateInstance(typeof(T));
            FileClass fc = FileClass.GetFileClassBymodel(itemObj);
            JArray ar = JArray.Parse(fc.ReadFile());
            foreach (var item in ar)
            {
                retList.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return retList;
        }

        public override object SelectScalar<TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> selectColumn)
        {
            return null;
        }

        public override T GetModel(T obj)
        {
            return null;
        }
    }
}
