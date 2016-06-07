using System;
using System.Collections.Generic;
using System.Text;
using PublicClass;

namespace PublicClass
{
    public class ModelKey
    {
        public string GetObjKey(IMODEL obj)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
