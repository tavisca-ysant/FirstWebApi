using FirstWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebApi
{
    public class Response
    {

        public List<string> Error { get; set; } = null;

        //public string errorMsg { get; set; } = null;

        public Book Book { get; set; } = null;
        private static Response _response { get; set; } = null;
        public static Response GetResponse()
        {
            if (_response == null)
                _response = new Response();
            return _response;
        }
    }
}
