using System;
using DK.Domain.DTO.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace DK.API
{

    /// <summary>
    /// A type that wraps either an <typeparamref name="TValue"/> instance or an <see cref="ActionResult"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the result.</typeparam>
    public sealed class GeneralResult : IConvertToActionResult
    {

        /// <summary>
        /// Initializes a new instance of <see cref="ActionResult{TValue}"/> using the specified <see cref="ActionResult"/>.
        /// </summary>
        /// <param name="result">The <see cref="ActionResult"/>.</param>
        public GeneralResult(ActionResult result)
        {
            //if (typeof(IActionResult).IsAssignableFrom(typeof(TValue)))
            //{
            //var error = Resources.FormatInvalidTypeTForActionResultOfT(typeof(TValue), "ActionResult<T>");
            //throw new ArgumentException(error);
            //}

            Result = result;
        }

        /// <summary>
        /// Gets the <see cref="ActionResult"/>.
        /// </summary>
        public ActionResult Result { get; }


        /// <summary>
        /// Implictly converts the specified <paramref name="result"/> to an <see cref="ActionResult{TValue}"/>.
        /// </summary>
        /// <param name="result">The <see cref="ActionResult"/>.</param>
        public static implicit operator GeneralResult(ActionResult result)
        {
            if (result is NOkObjectResult)
            {
                var xGeneralResult = new GeneralResponse
                {
                    xMessages = ((NOkObjectResult)result).xErrorMessages,
                    xResult = ((OkObjectResult)result)?.Value,
                    xStatus = ((NOkObjectResult)result).xErrorMessages == null || !((NOkObjectResult)result).xErrorMessages.Any()
                };

                return new GeneralResult(new OkObjectResult(xGeneralResult));
            }
            else if(result is OkObjectResult)
            {
                var xGeneralResult = new GeneralResponse
                {
                    xMessages = new List<string>(),
                    xResult = ((OkObjectResult)result)?.Value,
                    xStatus = true

                };

                return new GeneralResult(new OkObjectResult(xGeneralResult));
            }
            else if (result is BadRequestResult)
            {
                //TODO: Handle Error!
                return new GeneralResult(result); // Temprorary
            }
            else if (result is BadRequestObjectResult)
            {
                //TODO: Handle Error!
                return new GeneralResult(result); // Temprorary
            }
            else if (result is CreatedAtActionResult)
            {
                var xGeneralResult = new GeneralResponse
                {
                    xMessages = new List<string>(),
                    xResult = ((CreatedAtActionResult)result)?.Value,
                    xStatus = true

                };

                return new GeneralResult(new OkObjectResult(xGeneralResult));
            }        
            else if (result is NotFoundResult)
            {
                //TODO: Handle Error!
                return new GeneralResult(result); // Temprorary
            }
            else
            {
                return new GeneralResult(result);
            }
        }


        IActionResult IConvertToActionResult.Convert()
        {
            return Result ?? new StatusCodeResult(500);
        }
    }
}