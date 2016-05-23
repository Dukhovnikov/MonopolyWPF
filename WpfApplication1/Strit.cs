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
        /// Владелец.
        /// </summary>
        public UserData Owner
        {
            get; set;
        }

        /// <summary>
        /// Название улиццы.
        /// </summary>
        public string StritName { get; }

        /// <summary>
        /// Рента.
        /// </summary>
        public int[] Rent { get; }

        /// <summary>
        /// Количество домов.
        /// </summary>
        public byte HouseValue { get; set; }

        /// <summary>
        /// Находится ли в залоге.
        /// </summary>
        public bool IsLaid { get; set; }

        /// <summary>
        /// Цена за один дом.
        /// </summary>
        public int HousePrice { get; private set; }

        /// <summary>
        /// Конструктор, инициализирующий улицу.
        /// </summary>
        /// <param name="StritName">Название улицы</param>
        /// <param name="HousePrice">Цена за один дом</param>
        /// <param name="Rent">Рента</param>
        public Strit(string StritName, int HousePrice, int[] Rent)
        {
            this.StritName = StritName;
            this.HousePrice = HousePrice;
            this.Rent = Rent;
            IsLaid = false;
            HouseValue = 0;
        }

        
    }
}
