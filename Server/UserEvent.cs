using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1;

namespace Server
{
    class UserEvent
    {
        public static Action<UserData>[] UserEvents = new Action<UserData>[] { NewRound, OutOfPrisson, Chance, Soc };
        static Random rndChance = new Random();
        static Random rndSoc = new Random();

        static void OutOfPrisson(UserData befor)
        {
            befor.reason=" Оплата выхода из тюрьмы -50";
            befor.Deposit -= 50;
        }

        static void NewRound(UserData befor)
        {
            befor.reason = "Прохождение поля вперёд +200";
            befor.Deposit += 200;
        }

        static void Soc(UserData befor)
        {
            Action[] SC=new Action[]
            {
                ()=> { befor.reason="Банковская ошибка в вашу пользу получите 200"; befor.Deposit += 200; },
                ()=> { befor.reason = "Наследство получите 100"; befor.Deposit += 100; },
                ()=> { befor.reason = "Штраф за привышение скорости -15"; befor.Deposit -= 15; }
                ///TODO: Дополнить событиями
            };

            SC[rndSoc.Next(SC.Length)]();
        }

        static void Chance(UserData befor)
        {
            Action[] chnc = new Action[]
            {
                ()=> { befor.reason="Банковская ошибка в вашу пользу получите 200"; befor.Deposit += 200; },
                ()=> { befor.reason = "Наследство получите 100"; befor.Deposit += 100; },
                ()=> { befor.reason = "Штраф за привышение скорости -15"; befor.Deposit -= 15; }
                ///TODO: Дополнить событиями
            };

            chnc[rndSoc.Next(chnc.Length)]();
        }
    }
}
