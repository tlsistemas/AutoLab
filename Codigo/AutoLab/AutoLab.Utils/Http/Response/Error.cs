using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLab.Utils.Http.Response
{
    public class Error
    {
        public String Message { get; set; }
        public Error(String message)
        {
            Message = message;
        }
    }
}
