using Business.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

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
        
        
        ///<summary>
        ///List Authors
        ///</summary>
        ///<remarks>Authors</remarks>
        ///<return>List authors</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _authorService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        ///<summary>
        ///Get Author
        ///</summary>
        ///<remarks>Author</remarks>
        ///<return>author</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _authorService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Add Author.
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult Add([FromBody] AuthorAddDto author)
        {
            var newAuthor = new Author
            {
                Name = author.Name,
                Email = author.Email
            };
            var result = _authorService.Add(newAuthor);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        
        /// <summary>
        /// Delete Author.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _authorService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest("Hata oluştu");
        }
        
        ///<summary>
        ///Get Theses By Author Id
        ///</summary>
        ///<remarks>Theses</remarks>
        ///<return>theses</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThesisDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id:int}/theses")]
        public IActionResult GetThesesByAuthorId([FromRoute] int id)
        {
            var result = _authorService.GetThesesByAuthorId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
    }
}
