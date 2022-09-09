using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Models
{
    public class ReturnLoginModel
    {
        public string Token {get; set; }
        public string Id { get; set; }
        public string  Name { get; set; }
    }
}
