using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Middleware
{
    public class ApiErrorResponse
    {
        public int StatusCode {get; set;}
        public string Message {get; set;}
        public string Details {get; set;}
    }
}