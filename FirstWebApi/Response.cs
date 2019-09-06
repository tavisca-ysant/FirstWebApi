using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi
{
    public class Response
    {
        
        public List<string> Error { get; set; }
        public Book Book { get; set; }
    }
}
