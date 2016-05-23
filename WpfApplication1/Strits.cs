using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public
    class Strits
    {
        List<Strit> strits;

        public List<Strit> this[string name]
        {
            get
            {
                List<Strit> rez = new List<Strit>();
                var re =
                    from stq in strits
                    where stq.Owner.UserName == name
                    select stq;
                rez.Add(re as Strit);
                return rez;
            }
        }
    }
}
