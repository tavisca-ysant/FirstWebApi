//using FirstWebApi.Models;
//using FirstWebApi.Utility;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FirstWebApi.Middlewares
//{
//    public class ValidationMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private string bodyStr = "";
//        private Response response = Response.GetResponse();
//        public ValidationMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }
//        public async Task Invoke(HttpContext context)
//        {
//            var req = context.Request;
//            try { req.EnableRewind(); } catch { }
//            using (StreamReader reader
//                  = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
//            {
//                bodyStr = reader.ReadToEnd();
//            }
            
//            if (req.Method.Equals("POST")) {
//                Book book1 = JsonConvert.DeserializeObject<Book>(bodyStr);
//                if (!Validation.IsInvalidBook(book1))
//                {
//                    book1.Id = PrimaryKeyGenerator.GetID();
//                    response.Book = book1;
//                    response.Error = null;
//                }
//                else
//                {
//                    response.Error = new ErrorHandler().BookValidation(book1);
//                    response.Book = null;
//                }
//            }
//            else if (req.Method.Equals("GET") || req.Method.Equals("DELETE"))
//            {
//                var d = context.Request.Path.Value;
//                Debug.WriteLine(d);
//                var stringToParse = "";
//                if (d[d.LastIndexOf('/') + 1] == '-')
//                    stringToParse = d.Substring(d.LastIndexOf('/') + 1);
//                else stringToParse = d[d.LastIndexOf('/') + 1].ToString();
//                if (!Int32.TryParse(stringToParse, out int ID))
//                {
//                    response.Error = new List<string>();
//                    response.errorMsg = $"{ID} is not an integer";
//                    response.Error.Add(response.errorMsg);
//                }
//                Debug.WriteLine($"ID is {ID}");
//                if (!Validation.ValidateID(ID))
//                {
//                    response.Error = new List<string>();
//                    response.Error.Add(new ErrorHandler().ValidateID(ID));
//                }
                
//            }
//            else if (req.Method.Equals("PUT"))
//            {
//                var d = context.Request.Path.Value;
//                var stringToParse = "";
//                if (d[d.LastIndexOf('/') + 1] == '-')
//                    stringToParse = d.Substring(d.LastIndexOf('/') + 1);
//                else stringToParse = d[d.LastIndexOf('/') + 1].ToString();
//                if (!Int32.TryParse(stringToParse, out int ID))
//                {
//                    response.Error = new List<string>();
//                    response.errorMsg = $"{ID} is not an integer";
//                    response.Error.Add(response.errorMsg);
//                }
//                if (!Validation.ValidateID(ID))
//                {
//                    response.Error = new List<string>();
//                    response.Error.Add(new ErrorHandler().ValidateID(ID));
                    
//                }
//                else if(Validation.ValidateID(ID))
//                {
//                    Book book1 = JsonConvert.DeserializeObject<Book>(bodyStr);
//                    if (!Validation.IsInvalidBook(book1))
//                    {
//                        response.Book = book1;
//                        response.Error = null;
//                    }
//                    else
//                    {
//                        response.Error = new ErrorHandler().BookValidation(book1);
//                        response.Book = null;
//                    }
//                }
//            }

//            // Rewind, so the core is not lost when it looks the body for the request
//            req.Body.Position = 0;
//            await _next(context);
//            // context.Request.Body.Dipose() might be added to release memory, not tested
//        }



//    }
//}
