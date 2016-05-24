﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WpfApplication1
{
    /// <summary>
    /// Искусственный клас, реализующий информмацию о пользователе.
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName;

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        IPEndPoint _socket;

        int _deposit;
        public int Deposit
        {
            get
            {
                return _deposit;
            }
            set
            {
                _deposit = value;
                OnDepositChange(this);
            }
        }

        /// <summary>
        /// Получение адреса пользователя.
        /// </summary>
        public string getSocket()
        {
            return _socket.ToString();
        }

        int[] _strits;
        /// <summary>
        /// Улицы принадлежащие пользователю.
        /// </summary>
        public int[] StritNum
        {
            get
            {
                return _strits;
            }
            set
            {
                _strits = value;
                OnStritsChange(this);
            }
        }

        /// <summary>
        /// Функция возращающая список улиц, купленных пользователем.
        /// </summary>
        public List<Strit> StritList
        {
            get
            {
                var rez = new List<Strit>();
                foreach (var a in StritNum)
                {
                    rez.Add(Strits.strits[a]);
                }
                return rez;
            }
        }

        public string reason="";

        /// <summary>
        /// Конструктор класса задающий имя пользователя и его адрес.
        /// </summary>
        public UserData(string UserName, IPEndPoint _socket)
        {
            this.UserName = UserName;
            this._socket = _socket;
        }

        public event Action<UserData> OnDepositChange;
        public event Action<UserData> OnStritsChange;

        public override string ToString()
        {
            return UserName;
        }
    }
}
