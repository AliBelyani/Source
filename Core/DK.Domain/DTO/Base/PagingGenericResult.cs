using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Base
{
    public class PagingResult<T>
    {
        public IEnumerable<T> xData { get; set; }
        public long xCount { get; set; }
    }
}
