using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DK.API
{
    [ApiController]
    [Route("api/{controller}")]
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        public virtual NOkObjectResult Ok(object value, IEnumerable<string> xErrorMessages)
        {
            return new NOkObjectResult(value, xErrorMessages);
        }
    }

    public class NOkObjectResult : OkObjectResult
    {
        public IEnumerable<string> xErrorMessages { get; }
        public NOkObjectResult(object value, IEnumerable<string> xErrorMessages) : base(value)
        {
            this.xErrorMessages = xErrorMessages;
        }
    }
}
