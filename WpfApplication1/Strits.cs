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

        /// <summary>
        /// типа выбор по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Strit> this[string name]
        {
            get
            {
                var re =
                    from stq in strits
                    where stq.Owner.UserName == name
                    select stq;
                return re.ToList();
            }
        }
    }
}
