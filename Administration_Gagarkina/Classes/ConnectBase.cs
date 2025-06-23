using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administration_Gagarkina.DataBase;


namespace Administration_Gagarkina.Classes
{
    class ConnectBase
    {
        public static Overtime_ADMEntities4 entObj;

        static ConnectBase()
        {
            entObj = new Overtime_ADMEntities4();
        }
    }
}
