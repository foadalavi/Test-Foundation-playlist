using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyLibrary
{
    public class ObjectComparer
    {
        public bool Compare(object object1, object object2)
        {
            if (object1 == object2)
            {
                return true;
            }

            var p1 = object1.GetType().GetProperties();
            var p2 = object2.GetType().GetProperties();
            if (p1.Length == p2.Length                          // checkes the number of properties
                && p1.All(t => p2.Any(o => o.Name == t.Name))  // checkes if the property names are the same
                && p1.All(t=> p2.First(o => o.Name == t.Name).GetValue(object2) == t.GetValue(object1)) // checks the value of the properties
                )
            {
                return true;
            }

            return false;
        }
    }
}
