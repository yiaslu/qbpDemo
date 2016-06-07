using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace DBHelp
{
    public class JFExecute : BaseExecute
    {
        List<BaseMODEL> GetListModel(string str, Type mType)
        {
            List<BaseMODEL> retList = new List<BaseMODEL>();
            PropertyInfo[] pros = mType.GetProperties();
            JArray ar = JArray.Parse(str);
            foreach (var item in ar)
            {
                retList.Add((BaseMODEL)JsonConvert.DeserializeObject(ar.ToString(), mType));
            }
            return retList;
        }
        int InsertAll()
        {
            int retTrueCount = 0;
            addList = addList.OrderBy(info => info.GetType().Name).ToList();
            BaseMODEL topMod = null;
            List<BaseMODEL> toplist = new List<BaseMODEL>();
            foreach (var info in addList)
            {
                FileClass fs = FileClass.GetFileClassBymodel(info);
                if (topMod != null && topMod.GetType().Name == topMod.GetType().Name)
                {
                    toplist.Add(info);
                }
                else
                {
                    toplist = GetListModel(fs.ReadFile(), info.GetType());
                    toplist.Add(info);
                }
                fs.WriteInFile(JsonConvert.SerializeObject(toplist), true);
                retTrueCount++;
            }
            return retTrueCount;
        }
        int UpdateAll()
        {
            int retTrueCount = 0;
            addList = addList.OrderBy(info => info.GetType().Name).ToList();
            BaseMODEL topMod = null;
            List<BaseMODEL> toplist = new List<BaseMODEL>();
            foreach (var info in deleteList)
            {
                FileClass fs = FileClass.GetFileClassBymodel(info);
                if (topMod != null && topMod.GetType().Name == topMod.GetType().Name)
                {
                    deleteItem(toplist, info);
                    toplist.Add(info);
                }
                else
                {
                    toplist = GetListModel(fs.ReadFile(), info.GetType());
                    deleteItem(toplist, info);
                    toplist.Add(info);
                }
                fs.WriteInFile(JsonConvert.SerializeObject(toplist), true);
                retTrueCount++;
            }
            return retTrueCount;
        }
        int DeleteAll()
        {
            int retTrueCount = 0;
            addList = addList.OrderBy(info => info.GetType().Name).ToList();
            BaseMODEL topMod = null;
            List<BaseMODEL> toplist = new List<BaseMODEL>();
            foreach (var info in deleteList)
            {
                FileClass fs = FileClass.GetFileClassBymodel(info);
                if (topMod != null && topMod.GetType().Name == topMod.GetType().Name)
                {
                    deleteItem(toplist, info);
                }
                else
                {
                    toplist = GetListModel(fs.ReadFile(), info.GetType());
                    deleteItem(toplist, info);
                }
                fs.WriteInFile(JsonConvert.SerializeObject(toplist), true);
                retTrueCount++;
            }
            return retTrueCount;
        }

        void deleteItem(List<BaseMODEL> list, BaseMODEL info)
        {
            PropertyInfo[] p = info.GetType().GetProperties();
            List<KeyBox> kbox = GetKeyList(p, info);
            foreach (KeyBox item in kbox)
            {
                if (string.IsNullOrEmpty(item.Dbpara.Value + ""))
                {
                    throw new Exception("未将主键赋值");
                }
            }
            if (kbox.Count == 1)
            {
                var wlist = list.Where(s => s.GetType().GetProperties().Where(ps => ps.Name == kbox[0].Key && ps.GetValue(s, null) == kbox[0].Dbpara.Value).Count() == 1);
                foreach (var item in wlist)
                {
                    list.Remove(item);
                }
            }
            if (kbox.Count == 2)
            {
                var wlist = list.Where(s => s.GetType().GetProperties().Where(ps => ps.Name == kbox[0].Key && ps.GetValue(s, null) == kbox[0].Dbpara.Value).Count() == 1 && s.GetType().GetProperties().Where(ps => ps.Name == kbox[1].Key && ps.GetValue(s, null) == kbox[1].Dbpara.Value).Count() == 1);
                foreach (var item in wlist)
                {
                    list.Remove(item);
                }
            }
        }

        public override int DBCommit()
        {
            int retNum = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                retNum += InsertAll();
                retNum += UpdateAll();
                retNum += DeleteAll();
                Clear();
                scope.Complete();
            }
            return retNum;
        }
    }
}