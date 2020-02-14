using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupBB.Controller
{
    public class GmailSettings
    {
        private string _host = "smtp.gmail.com"; // smtp.mail.ru // smtp.gmail.com
        private int _port = 587;// 587/25 
        private string _password = "fcgbhbyBB";

        public string Password()
        {
            return _password;
        }
        public string Host()
        {
            return _host;
        }
        public int Port()
        {
            return _port;
        }

    }
}
