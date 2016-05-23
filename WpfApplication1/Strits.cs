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
        public static List<Strit> strits = new List<Strit>
                    {
            new Strit(Strit.Color.Коричневый,"УЛ. ЖИТНАЯ", 60, 50, new int[6] {2,10,30,90,160,250}),
            new Strit(Strit.Color.Коричневый,"УЛ. НАГАТИНСКАЯ", 60, 50, new int[6] {4,20,60,180,320,450}),
            new Strit(Strit.Color.ЖД,"РИЖСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 200, 0, new int[6] {25,50,100,200,0,0}),
            new Strit(Strit.Color.Голубой,"ВАРШАВСКОЕ ШОССЕ", 100, 50, new int[6] {6,30,90,270,400,550}),
            new Strit(Strit.Color.Голубой,"УЛ. ОГАРЕВА", 100, 50, new int[6] {6,30,90,270,400,550}),
            new Strit(Strit.Color.Голубой,"УЛ. ПЕРВАЯ ПАРКОВАЯ", 120, 50, new int[6] {8,40,100,300,450,600}),
            new Strit(Strit.Color.Розовый,"УЛ. ПОЛЯНКА", 140, 100, new int[6] {10,50,150,450,625,750}),
            new Strit(Strit.Color.Электростанция ,"ЭЛЕКТРОСТАНЦИЯ", 150, 0, new int[6] {4,10,0,0,0,0}),
            new Strit(Strit.Color.Голубой,"УЛ. СРЕТЕНКА", 140, 100, new int[6] {10,50,150,450,625,750}),
            new Strit(Strit.Color.Голубой,"РОСТОВСКАЯ НАБЕРЕЖНАЯ", 160, 100, new int[6] {12,60,180,500,700,900}),
            new Strit(Strit.Color.ЖД,"КУРСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 200, 0, new int[6] {25,50,100,200,0,0}),
            new Strit(Strit.Color.Оранжевый,"РЯЗАНСКИЙ ПРОСПЕКТ", 180, 100, new int[6] {14,70,200,550,750,950}),
            new Strit(Strit.Color.Оранжевый,"УЛ. ВАВИЛОВА", 180, 100, new int[6] {14,70,200,550,750,950}),
            new Strit(Strit.Color.Оранжевый,"РУБЛЕВСКОЕ ШОССЕ", 200, 100, new int[6] {16,80,220,600,800,1000}),
            new Strit(Strit.Color.Красный,"УЛ. ТВЕРСКАЯ", 220, 150, new int[6] {18,90,250,700,875,1050}),
            new Strit(Strit.Color.Красный,"УЛ. ПУШКИНСКАЯ", 220, 150, new int[6] {18,90,250,700,875,1050}),
            new Strit(Strit.Color.Красный,"ПЛОЩАДЬ МАЯКОВСКОГО", 240, 150, new int[6] {20,100,300,750,925,1100}),
            new Strit(Strit.Color.ЖД,"КАЗАНСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 200, 0, new int[6] {25,50,100,200,0,0}),
            new Strit(Strit.Color.Желтый,"УЛ. ГРУЗИНСКИЙ ВАЛ", 260, 150, new int[6] {22,110,330,800,975,1150}),
            new Strit(Strit.Color.Желтый,"УЛ. ЧАЙКОВСКОГО", 260, 150, new int[6] {22,110,330,800,975,1150}),
            new Strit(Strit.Color.Водопровод ,"ВОДОПРОВОД", 150, 0, new int[6] {4,10,0,0,0,0}),
            new Strit(Strit.Color.Желтый,"СМОЛЕНСКАЯ ПЛОЩАДЬ", 280, 150, new int[6] {24,120,360,850,1025,1200}),
            new Strit(Strit.Color.Зеленый,"УЛ. ЩУСЕВА", 300, 200, new int[6] {26,130,390,900,1100,1275}),
            new Strit(Strit.Color.Зеленый,"ГОГОЛЕВСКИЙ БУЛЬВАР", 300, 200, new int[6] {26,130,390,900,1100,1275}),
            new Strit(Strit.Color.Зеленый,"КУТУЗОВСКИЙ ПРОСПЕКТ", 320, 200, new int[6] {28,150,450,1000,1200,1400}),
            new Strit(Strit.Color.ЖД,"ЛЕНИНГРАДСКАЯ ЖЕЛЕЗНАЯ ДОРОГА", 200, 0, new int[6] {25,50,100,200,0,0}),
            new Strit(Strit.Color.Синий,"УЛ. МАЛАЯ БРОННАЯ", 350, 200, new int[6] {35,175,500,1100,1300,1500}),
            new Strit(Strit.Color.Синий,"УЛ. АРБАТ", 400, 200, new int[6] {50,200,600,1400,1700,2000}),
        };

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

        public Strits()
        {
            ///TODO: 
            ///тут надо как то инициализировать все улицы в список strits
        }
    }


}
