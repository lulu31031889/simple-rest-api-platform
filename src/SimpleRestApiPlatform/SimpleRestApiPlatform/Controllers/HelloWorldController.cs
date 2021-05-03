using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRestApiPlatform.Controllers
{
    /// <summary>
    /// Just a simple endpoint to get started.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HelloWorldController : ControllerBase
    {
        /// <summary>
        /// Just returns "Hello World!".
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/HelloWorld
        /// </remarks>
        /// <returns>"Hello World!"</returns>
        /// <response code="200">Returns "Hello World!".</response>
        /// <response code="405">Returns 405 (Method Not Allowed) if you attempt any verb other than GET.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(Ok("Hello World!"));
        }
    }
}
