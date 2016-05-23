using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Strit
    {
        public enum Color
        {
            Коричневый,
            Голубой,
            Розовый,
            Оранжевый,
            Красный,
            Желтый, 
            Зеленый,
            Синий,
            ЖД,
            Электростанция,
            Водопровод
        }

        public Color Type { get; private set; }

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
        /// Цена за одну улицу.
        /// </summary>
        public int StreetPrice { get; private set; }

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
        public Strit(Color Type ,string StritName, int StreetPrice ,int HousePrice, int[] Rent)
        {
            this.Type = Type;
            this.StritName = StritName;
            this.StreetPrice = StreetPrice;
            this.HousePrice = HousePrice;
            this.Rent = Rent;
            IsLaid = false;
            HouseValue = 0;
        }

        public override string ToString()
        {
            return StritName;
        }
    }
}
