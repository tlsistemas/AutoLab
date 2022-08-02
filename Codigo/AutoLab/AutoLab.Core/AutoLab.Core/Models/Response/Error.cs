using System;

namespace AutoLab.Core.Models.Response
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
