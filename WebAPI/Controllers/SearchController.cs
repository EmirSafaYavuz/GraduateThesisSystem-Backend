using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using DataAccess.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        ///<summary>
        /// Search Theses by Title
        ///</summary>
        ///<remarks>Search for theses by their titles.</remarks>
        ///<param name="query">The search query.</param>
        ///<param name="thesisType">Optional: The type of the thesis.</param>
        ///<returns>A list of theses matching the search criteria.</returns>
        ///<response code="200">Returns the list of theses.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThesisDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Thesis/Title")]
        public IActionResult SearchThesisTitle(string query, ThesisType? thesisType)
        {
            var result = _searchService.SearchThesisTitle(query, thesisType);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Theses by Abstract
        ///</summary>
        ///<remarks>Search for theses by their abstracts.</remarks>
        ///<param name="query">The search query.</param>
        ///<param name="thesisType">Optional: The type of the thesis.</param>
        ///<returns>A list of theses matching the search criteria.</returns>
        ///<response code="200">Returns the list of theses.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThesisDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Thesis/Abstract")]
        public IActionResult SearchThesisAbstract(string query, ThesisType? thesisType)
        {
            var result = _searchService.SearchThesisAbstract(query, thesisType);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Theses by Number
        ///</summary>
        ///<remarks>Search for theses by their numbers.</remarks>
        ///<param name="query">The search query.</param>
        ///<param name="thesisType">Optional: The type of the thesis.</param>
        ///<returns>A list of theses matching the search criteria.</returns>
        ///<response code="200">Returns the list of theses.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ThesisDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Thesis/No")]
        public IActionResult SearchThesisNo(string query, ThesisType? thesisType)
        {
            var result = _searchService.SearchThesisNo(query, thesisType);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Authors
        ///</summary>
        ///<remarks>Search for authors by name.</remarks>
        ///<param name="query">The search query.</param>
        ///<returns>A list of authors matching the search criteria.</returns>
        ///<response code="200">Returns the list of authors.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Author")]
        public IActionResult SearchAuthor(string query)
        {
            var result = _searchService.SearchAuthor(query);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Institutes
        ///</summary>
        ///<remarks>Search for institutes by name.</remarks>
        ///<param name="query">The search query.</param>
        ///<returns>A list of institutes matching the search criteria.</returns>
        ///<response code="200">Returns the list of institutes.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Institute>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Institute")]
        public IActionResult SearchInstitute(string query)
        {
            var result = _searchService.SearchInstitute(query);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Supervisors
        ///</summary>
        ///<remarks>Search for supervisors by name.</remarks>
        ///<param name="query">The search query.</param>
        ///<returns>A list of supervisors matching the search criteria.</returns>
        ///<response code="200">Returns the list of supervisors.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SupervisorDetailDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Supervisor")]
        public async Task<IActionResult> SearchSupervisor(string query)
        {
            var result = await _searchService.SearchSupervisor(query);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Subject Topics
        ///</summary>
        ///<remarks>Search for subject topics by name.</remarks>
        ///<param name="query">The search query.</param>
        ///<returns>A list of subject topics matching the search criteria.</returns>
        ///<response code="200">Returns the list of subject topics.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubjectTopic>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("SubjectTopic")]
        public IActionResult SearchSubjectTopic(string query)
        {
            var result = _searchService.SearchSubjectTopic(query);
            return HandleResult(result);
        }

        ///<summary>
        /// Search Keywords
        ///</summary>
        ///<remarks>Search for keywords by name.</remarks>
        ///<param name="query">The search query.</param>
        ///<returns>A list of keywords matching the search criteria.</returns>
        ///<response code="200">Returns the list of keywords.</response>
        ///<response code="400">If the request is invalid or there is an error in the search process.</response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Keyword>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("Keyword")]
        public IActionResult SearchKeyword(string query)
        {
            var result = _searchService.SearchKeyword(query);
            return HandleResult(result);
        }

        private IActionResult HandleResult<T>(IDataResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}