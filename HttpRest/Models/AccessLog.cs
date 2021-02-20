using System;

namespace Calculator.HttpRest.Models
{
    public class AccessLog
    {
        public string IpAddress { get; set; }
        public DateTime dateOfAccess { get; set; }
    }
}