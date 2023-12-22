using Business.Abstract;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordsController : ControllerBase
    {
        private readonly IKeywordService _keywordService;

        public KeywordsController(IKeywordService keywordService)
        {
            _keywordService = keywordService;
        }
        
        ///<summary>
        ///List Keywords
        ///</summary>
        ///<remarks>Keyword</remarks>
        ///<return>List keywords</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Keyword>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _keywordService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Add Keyword.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Keyword))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult Add([FromBody] Keyword keyword)
        {
            var result = _keywordService.Add(keyword);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Delete Keyword.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _keywordService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest("Hata oluştu");
        }
    }
}
