using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleRestApiPlatform.Core.Interfaces.Services;
using System;

namespace SimpleRestApiPlatform.Controllers
{
    /// <summary>
    /// Endpoints to test returning signed integers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RandomNumbersController : ControllerBase
    {
        readonly IRandomNumberGeneratorService _randomNumberGeneratorService;

        public RandomNumbersController(IRandomNumberGeneratorService randomNumberGeneratorService)
        {
            _randomNumberGeneratorService = randomNumberGeneratorService;
        }

        /// <summary>
        /// Return a random single integer between -2147483648 and 2147483647.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/RandomNumbers/GetRandomInteger
        /// </remarks>
        /// <response code="200">Returns a random integer between -2147483648 and 2147483647.</response>
        /// <response code="405">Returns 405 (Method Not Allowed) if you attempt any verb other than GET.</response>
        [HttpGet("GetRandomInteger")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
        public IActionResult GetRandomInteger()
        {
            return Ok(_randomNumberGeneratorService.GenerateRandomSigned32BitInteger());
        }

        /// <summary>
        /// Return a single random integer between two given strings that are integers.
        /// </summary>
        /// <remarks>
        /// Sample request (NOTE the quotes around the numbers in this request we pass strings):
        /// 
        /// GET /api/RandomNumbers/GetRandomIntegerBetweenTwoStringIntegerValues
        /// {
        ///     "minimumValue" : "-100",
        ///     "maximumValue" : "100"
        /// }
        /// </remarks>
        /// <param name="minimumValue">The lowest value for the random number (as a number e.g. -100).</param>
        /// <param name="maximumValue">The highest value for the random number (as a number e.g. 100).</param>
        /// <returns>A random integer between the "minimumValue" and "maximumValue".</returns>
        /// <response code="200">Returns a single random integer.</response>
        /// <response code="400">Returns error and detail if one (or more) parameters was not an integer, empty, or out of range.</response>
        /// <response code="405">Returns 405 (Method Not Allowed) if you attempt any verb other than GET.</response>
        /// /// <response code="422">Returns error if maximum is smaller value than minimum value.</response>
        [HttpGet("GetRandomIntegerBetweenTwoStringIntegerValues")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public IActionResult GetRandomInteger(string minimumValue, string maximumValue)
        {
            try
            {
                var minVal = int.Parse(minimumValue);
                var maxVal = int.Parse(maximumValue);

                return Ok(_randomNumberGeneratorService.GenerateRandomSigned32BitInteger(minVal, maxVal));
            }
            catch(ArgumentOutOfRangeException e)
            {
                return Problem(detail: "Minimum value greater than maximum value.",
                    statusCode: 422,
                    title: "Maximum value must be greater than minimum value.",
                    type: e.GetType().ToString());
            }
            catch (ArgumentException e)
            {
                return Problem(detail: "One or more parameters might be empty.",
                    statusCode: 400,
                    title: "There was an issue with one (or more) of the parameters.",
                    type: e.GetType().ToString());
            }
            catch (FormatException e)
            {
                return Problem(detail: "One or more parameters is not an integer.",
                    statusCode: 400,
                    title: "There was an issue with one (or more) of the parameters.",
                    type: e.GetType().ToString());
            }
            catch (OverflowException e)
            {
                return Problem(detail: "Either 'minimumValue' is less than -2147483648, or 'maximumValue is greater than 2147483647.",
                    statusCode: 400,
                    title: "There was an issue with one (or more) of the parameters.",
                    type: e.GetType().ToString());
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message,
                    statusCode: 400,
                    title: "There was an unknown issue.",
                    type: e.GetType().ToString());
            }
        }

        /// <summary>
        /// Return a single random integer between two given integers.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/RandomNumbers/GetRandomIntegerBetweenTwoNumericIntegerValues
        /// {
        ///     "minimumValue" : -100,
        ///     "maximumValue" : 100
        /// }
        /// </remarks>
        /// <param name="minimumValue">The lowest value for the random number (as a number e.g. -100).</param>
        /// <param name="maximumValue">The highest value for the random number (as a number e.g. 100).</param>
        /// <returns>A random integer between the "minimumValue" and "maximumValue".</returns>
        /// <response code="200">Returns a single random integer.</response>
        /// <response code="400">Returns error and detail if one (or more) parameters was not an integer, empty, or out of range.</response>
        /// <response code="405">Returns 405 (Method Not Allowed) if you attempt any verb other than GET.</response>
        /// <response code="422">Returns error if maximum is smaller value than minimum value.</response>
        [HttpGet("GetRandomIntegerBetweenTwoNumericIntegerValues")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public IActionResult GetRandomInteger(int minimumValue, int maximumValue)
        {
            try
            {
                return Ok(_randomNumberGeneratorService.GenerateRandomSigned32BitInteger(minimumValue, maximumValue));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return Problem(detail: "Minimum value greater than maximum value.",
                    statusCode: 422,
                    title: "Maximum value must be greater than minimum value.",
                    type: e.GetType().ToString());
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message,
                    statusCode: 400,
                    title: "There was an unknown issue.",
                    type: e.GetType().ToString());
            }
        }
    }
}
