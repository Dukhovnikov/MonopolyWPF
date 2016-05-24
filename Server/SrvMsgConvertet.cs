using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public
    class SrvMsgConvertet
    {
        public static string Create(string[] args)
        {
            string res = "";
            foreach (var s in args)
            {
                res += s.ToString() + ':';
            }
            res = res.Substring(0, res.Length - 1);
            return res;
        }

        public enum OutMsgType
        {
            /// <summary>
            /// Системное сообщение.
            /// </summary>
            SystemMsg,
            /// <summary>
            /// Обновление баланса.
            /// </summary>
            DepositUpdate,
            /// <summary>
            /// Обновление недвижимости.
            /// </summary>
            StritsUpdate,
            /// <summary>
            /// Запрос на покупку.
            /// </summary>
            AskSelling,
            /// <summary>
            /// Начат аукцион.
            /// </summary>
            AuctionStart,
            /// <summary>
            /// Запрошенная улица имеет другого владельца.
            /// </summary>
            OtherOwner
        }

        /// <summary>
        /// Типа входящих сообщений.
        /// </summary>
        public enum InMsgType
        {
            /// <summary>
            /// Обновление баланса
            /// </summary>
            DepositUpdate = 1,
            /// <summary>
            /// обновление недвижимости.
            /// </summary>
            /// <remarks> Покупка дома</remarks>
            BayHouse,
            /// <summary>
            /// Запрос на покупку.
            /// </summary>
            AskSelling,
            /// <summary>
            /// Ставка на аукцион.
            /// </summary>
            Auction,
            /// <summary>
            /// Оплата ренты.
            /// </summary>
            Rent,
            /// <summary>
            /// Подтверждение сделки (запрос на обмен).
            /// </summary>
            acceptDeal,
            /// <summary>
            /// Заложить улицу.
            /// </summary>
            establish
        }

        #region События
        /// <summary>
        /// Обновление баланса
        /// </summary>
        /// <remarks>юзер,значение,причина </remarks>
        public static event Action<byte, int, string> DepositUpdate;
        /// <summary>
        /// Покупка дома.
        /// </summary>
        /// <remarks> юзер, улица</remarks>
        public static event Action<byte, byte> BayHouse;
        /// <summary>
        /// Запрос на покупку.
        /// </summary>
        /// <remarks> юзер, улица</remarks>
        public static event Action<byte, byte> AskSelling;
        /// <summary>
        /// Ставка на аукцион.
        /// </summary>
        /// <remarks> юзер, ставка</remarks>
        public static event Action<byte, int> Auction;
        /// <summary>
        /// Оплата ренты.
        /// </summary>
        /// <remarks> юзер, улица</remarks>
        public static event Action<byte, byte> Rent;
        /// <summary>
        /// Подтверждение сделки (запрос на обмен).
        /// </summary>
        /// <remarks> юзер-продовец, юзер-покупатель, улица, деньги</remarks>
        public static event Action<byte, byte, byte, int> acceptDeal;
        /// <summary>
        /// Заложить улицу.
        /// </summary>
        /// <remarks> юзер, улица</remarks>
        public static event Action<byte, byte> establish;

        public static event Action<string> Error;
        #endregion

        public static void Parse(string msg)
        {
            try
            {
                string[] Args = msg.Split(':');
                switch (byte.Parse(Args[1]))
                {
                    case ((byte)InMsgType.acceptDeal):
                        acceptDeal(byte.Parse(Args[0]), byte.Parse(Args[2]), byte.Parse(Args[3]), int.Parse(Args[4]));
                        break;
                    case ((byte)InMsgType.AskSelling):
                        AskSelling(byte.Parse(Args[0]), byte.Parse(Args[2]));
                        break;
                    case ((byte)InMsgType.Auction):
                        Auction(byte.Parse(Args[0]), int.Parse(Args[2]));
                        break;
                    case ((byte)InMsgType.DepositUpdate):
                        DepositUpdate(byte.Parse(Args[0]), int.Parse(Args[2]), Args[3]);
                        break;
                    case ((byte)InMsgType.establish):
                        establish(byte.Parse(Args[0]), byte.Parse(Args[2]));
                        break;
                    case ((byte)InMsgType.Rent):
                        Rent(byte.Parse(Args[0]), byte.Parse(Args[2]));
                        break;
                    case ((byte)InMsgType.BayHouse):
                        BayHouse(byte.Parse(Args[0]), byte.Parse(Args[2]));
                        break;
                    default:
                        throw new Exception("Не известный тип входящего сообщения");
                }
            }
            catch (Exception ex)
            {
                Error?.Invoke(ex.Message);
            }
        }
    }
}
