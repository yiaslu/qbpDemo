using PublicClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Reflection;
using System.Data.Common;
using Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace DBHelp.DataExecute
{
    public class EFQuery<T> : BaseQuery<T> where T : BaseMODEL, new()
    {
        static bool isLoad = false;

        public EFQuery()
        {
            Load();
        }
        private void Load()
        {
            if (_dbContext == null)
            {
                _dbContext = new EFEntities(connStr);
                if (!isLoad)
                {
                    var objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
                    var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                    mappingCollection.GenerateViews(new List<EdmSchemaError>());
                    isLoad = true;
                }
            }
        }
        private DbContext _dbContext = null;
        private DbContext dbContext
        {
            get
            {
                Load();
                return _dbContext;
            }
        }


        public override List<T> SelectList(string orderbystr)
        {
            List<object> values = new List<object>();
            int js = 0;
            foreach (var item in listDara)
            {
                values.Add(item.Value);
                WhereStr = WhereStr.Replace(item.Key, "@" + js);
                js++;
            }
            if (!string.IsNullOrEmpty(WhereStr))
                return dbContext.Set<T>().Where("1=1" + WhereStr, values.ToArray()).ToList();
            if (!string.IsNullOrEmpty(orderbystr))
                return dbContext.Set<T>().OrderBy(orderbystr).ToList();
            if (!string.IsNullOrEmpty(orderbystr) && !string.IsNullOrEmpty(WhereStr))
                return dbContext.Set<T>().Where("1=1 " + WhereStr, values.ToArray()).OrderBy(orderbystr).ToList();

            WhereStr = "";
            listDara.Clear();
            return dbContext.Set<T>().ToList();
        }

        public override T GetModel(T obj)
        {
            Type type = typeof(T);
            PropertyInfo[] p = type.GetProperties();
            List<KeyBox> kbox = GetKeyList(p, obj);
            string whereStr = "";
            int js = 0;
            List<DbParameter> listPara = new List<DbParameter>();
            foreach (KeyBox box in kbox)
            {
                whereStr += (js == 0 ? "" : " AND ") + box.Key + "=" + box.Dbpara.ParameterName;
                listPara.Add(box.Dbpara);
                js++;
            }
            var retList = dbContext.Set<T>().Where(whereStr).ToList();
            if (retList.Count == 1)
                return retList[0];
            return new T();
        }
    }
}
