using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.HandlerResponse
{
    public class customException : Response
    {
        public customException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
