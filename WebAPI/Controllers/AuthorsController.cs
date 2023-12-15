using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        public IActionResult Get()
        {
            var result = _authorService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
    }
}
