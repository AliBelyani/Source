using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Base
{
    public class GeneralResponse<T>
    {
        public bool xStatus { get; set; } = true;
        public IEnumerable<string> xMessages { get; set; }
        public T xResult { get; set; }

    }
    public class GeneralResponse
    {
        public bool xStatus { get; set; }
        public IEnumerable<string> xMessages { get; set; }
        public object xResult { get; set; }
    }
}
