using Business.Abstract;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectTopicsController : ControllerBase
    {
        private readonly ISubjectTopicService _subjectTopicService;

        public SubjectTopicsController(ISubjectTopicService subjectTopicService)
        {
            _subjectTopicService = subjectTopicService;
        }
        
        ///<summary>
        ///List Subject Topics
        ///</summary>
        ///<remarks>SubjectTopics</remarks>
        ///<return>List subject topics</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubjectTopic>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _subjectTopicService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Add Subject Topic.
        /// </summary>
        /// <param name="subjectTopic"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectTopic))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult Add([FromBody] SubjectTopic subjectTopic)
        {
            var result = _subjectTopicService.Add(subjectTopic);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Hata oluştu");
        }
        
        /// <summary>
        /// Delete Subject Topic.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _subjectTopicService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest("Hata oluştu");
        }
    }
}
