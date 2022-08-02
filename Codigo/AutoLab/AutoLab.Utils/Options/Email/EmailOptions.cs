using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Utils.Options.Email
{
    public class EmailOptions
    {
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Smtp { get; set; }
        public int Timeout { get; set; }
        public String Host { get; set; }
        public String EmailTo { get; set; }
        public String EmailFrom { get; set; }

        public String ApiKey { get; set; }
    }
}
