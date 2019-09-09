using FirstWebApi.Models;
using FirstWebApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWebApi.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private string bodyStr = "";
        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var req = context.Request;
            try { req.EnableRewind(); } catch { }
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }
            //if(req.Method.Equals("POST"))
            //    bodyStr = bodyStr.Substring(1, bodyStr.Length - 2);
            if (req.Method.Equals("POST")) {
                Debug.WriteLine(bodyStr);
                Book book1 = JsonConvert.DeserializeObject<Book>(bodyStr);
                Debug.WriteLine("Book name is " + book1.Name);
                if (!Validation.IsInvalidBook(book1))
                    await _next(context);
                else return;
            }
            

            //Debug.WriteLine("Book name in middleware is "+book1.Name);
            // Rewind, so the core is not lost when it looks the body for the request
            req.Body.Position = 0;
            await _next(context);
            // context.Request.Body.Dipose() might be added to release memory, not tested
        }

    }
}
