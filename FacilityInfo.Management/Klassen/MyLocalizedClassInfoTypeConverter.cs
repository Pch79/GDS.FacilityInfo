using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityInfo.Management.Klassen
{
    class MyLocalizedClassInfoTypeConverter : LocalizedClassInfoTypeConverter
    {
        public MyLocalizedClassInfoTypeConverter()
        {
            AllowAddNonPersistentObjects = true;
            
        }

        public override void AddCustomItems(List<Type> list)
        {
            //list.Add(typeof(MyType));
            // list.AddRange(GetTypes());
            list.Clear();
            list.Add(typeof(String));
            list.Add(typeof(Int32));
            list.Add(typeof(Int16));
            list.Add(typeof(Decimal));
            list.Add(typeof(Double));
            list.Add(typeof(float));

            list.Sort(this);
            base.AddCustomItems(list);
        }

        public List<Type> GetTypes()
        {

            List<Type> lstResult = new List<Type>();
            lstResult = typeof(int).Assembly.GetTypes().Where(t => t.IsPrimitive).ToList();
            return lstResult;
        }

    }
}
