using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WpfApplication1
{
    public
    class UserData
    {
        public string UserName;
        IPEndPoint _socket;

        public string getSocket()
        {
            return _socket.ToString();
        }

        public int[] StritNum { get; set; }

        public List<Strit> StritList
        {
            get
            {
                var rez = new List<Strit>();
                foreach(var a in StritNum)
                {
                    rez.Add(Strits.strits[a]);
                }
                return rez;
            }
        }
    }
}
