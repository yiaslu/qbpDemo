using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Models
{
    public class EFEntities : DbContext
    {
        public EFEntities(string ConStr) : base(ConStr) { }
        public virtual DbSet<TheSysTableInfo> TheSysTableInfo { get; set; }
    }
}
