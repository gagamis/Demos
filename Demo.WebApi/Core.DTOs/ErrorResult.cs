using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
