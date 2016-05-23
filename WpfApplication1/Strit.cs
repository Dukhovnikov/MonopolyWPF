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
        /// <summary>
        /// Владелец
        /// </summary>
        public UserData Owner
        {
            get; set;
        }

        public string StritName { get; }

        /// <summary>
        /// рента
        /// </summary>
        public int[] Rent { get; }
        /// <summary>
        /// количество домов
        /// </summary>
        public int HouseValue
        {
            get; set;
        }
        /// <summary>
        /// находится ли в залоге
        /// </summary>
        public bool IsLaid = false;

        /// <summary>
        /// цена за один дом
        /// </summary>
        public int HousePrice { get; private set; }

        public Strit()
        {
            StritName = "RND";
            Rent = new int[6] { 0, 0, 0, 0, 0, 0 };

        }
    }
}
