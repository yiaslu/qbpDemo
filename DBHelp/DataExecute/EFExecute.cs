using PublicClass;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DBHelp.DataExecute
{
    public class EFExecute : BaseExecute
    {
        public EFExecute()
        {
            Committed = false;
        }



        #region 操作方法
        int AddModel(IMODEL obj)
        {
            obj = (IMODEL)dbContext.Set(obj.GetType()).Add(obj);
            return 1;
        }
        int UpdateModel(IMODEL obj)
        {
            dbContext.Entry(obj).State = EntityState.Modified;
            return 1;
        }
        int DeleteModel(IMODEL obj)
        {
            dbContext.Entry(obj).State = EntityState.Deleted;
            return 1;
        }
        int ExecuteNonQuery(string comText, List<DataParameter> whereList)
        {
            return dbContext.Database.ExecuteSqlCommand(comText, whereList);
        }
        #endregion

        #region 事务方法

        private readonly object _sync = new object();

        private DbContext dbContext = new DbContext(connStr);

        private DbContextTransaction _transaction = null;
        object Db { get; set; }
        bool Committed { get; set; }


        void Dispose()
        {
            dbContext.Dispose();
        }

        void Rollback()
        {
            Committed = false;
            if (_transaction != null)
                _transaction.Rollback();
        }


        DbContextTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        DbContextTransaction BeginTransaction()
        {
            if (_transaction == null)
            {
                Committed = false;
                _transaction = dbContext.Database.BeginTransaction(); ;
            }
            return _transaction;
        }

        #endregion




        public override int DBCommit()
        {
            int retNum = 0;
            if (!Committed)
            {
                lock (_sync)
                {
                    dbContext.SaveChanges();

                    if (_transaction != null)
                    {
                        _transaction.Commit();
                        _transaction = null;
                    }

                }
                Committed = true;
            }

            try
            {
                foreach (ExecuteBox box in exceList)
                {
                    retNum += ExecuteNonQuery(box.ExecSql, box.WhereList);
                }
                foreach (IMODEL model in addList)
                {
                    retNum += AddModel(model);
                }
                foreach (IMODEL model in updateList)
                {
                    retNum += UpdateModel(model);
                }
                foreach (IMODEL model in deleteList)
                {
                    retNum += DeleteModel(model);
                }

                dbContext.SaveChanges();
                return retNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
