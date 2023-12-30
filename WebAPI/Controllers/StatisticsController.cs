using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IInstituteService _instituteService;
        private readonly ILanguageService _languageService;
        private readonly ISupervisorService _supervisorService;
        private readonly IThesisService _thesisService;
        private readonly IUniversityService _universityService;
        private readonly ILocationService _locationService;
        private readonly ISubjectTopicService _subjectTopicService;
        private readonly IKeywordService _keywordService;

        public StatisticsController(IAuthorService authorService, IInstituteService instituteService, ILanguageService languageService, ISupervisorService supervisorService, IThesisService thesisService, IUniversityService universityService, ILocationService locationService, ISubjectTopicService subjectTopicService, IKeywordService keywordService)
        {
            _authorService = authorService;
            _instituteService = instituteService;
            _languageService = languageService;
            _supervisorService = supervisorService;
            _thesisService = thesisService;
            _universityService = universityService;
            _locationService = locationService;
            _subjectTopicService = subjectTopicService;
            _keywordService = keywordService;
        }

        [HttpGet("GetCounts")]
        public IActionResult GetCounts()
        {
            var counts = new
            {
                AuthorsCount = _authorService.GetCount().Data,
                InstitutesCount = _instituteService.GetCount().Data,
                LanguagesCount = _languageService.GetCount().Data,
                SupervisorsCount = _supervisorService.GetCount().Data,
                ThesesCount = _thesisService.GetCount().Data,
                UniversitiesCount = _universityService.GetCount().Data,
                LocationsCount = _locationService.GetCount().Data,
                SubjectTopicsCount = _subjectTopicService.GetCount().Data,
                KeywordsCount =  _keywordService.GetCount().Data
            };

            return Ok(counts);
        }
    }
}
