using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi.Utility
{
    public class PrimaryKeyGenerator
    {
        public static int ID = 0;

        public static int GetID()
        {
            return ++ID;
        }
    }
}
