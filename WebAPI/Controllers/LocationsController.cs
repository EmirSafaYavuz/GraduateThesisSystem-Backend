using Business.Abstract;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        
        ///<summary>
        ///List Locations
        ///</summary>
        ///<remarks>Locations</remarks>
        ///<return>List locations</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Location>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _locationService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Add Location.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Location))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult Add([FromBody] Location location)
        {
            var result = _locationService.Add(location);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Delete Location.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _locationService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest("Hata oluştu");
        }
    }
}
