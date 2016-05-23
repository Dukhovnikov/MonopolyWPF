using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public
    class Strit
    {
        public UserData Owner
        {
            get; set;
        }

        public int[] Rent { get; set; }
        public int HouseValue;
        public bool IsLaid = false;
        public int HousePrice { get; private set; }
    }
}
