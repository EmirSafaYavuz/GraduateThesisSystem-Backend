using Core.Entities;
using DataAccess.Entities.Enums;

namespace DataAccess.Entities.Dtos;

public class ThesisAddDto : IDto
{
    public int ThesisNo { get; set; }

    public string Title { get; set; } = null!;

    public string Abstract { get; set; } = null!;

    public int Year { get; set; }

    public int NumOfPages { get; set; }

    public int AuthorId { get; set; }

    public int LanguageId { get; set; }

    public int? CoSupervisorId { get; set; }

    public int InstituteId { get; set; }

    public int SupervisorId { get; set; }
    public IEnumerable<int>? SupervisorIdList { get; set; } = null!;
    public IEnumerable<int>? SubjectTopicIdList { get; set; } = null!;
    public IEnumerable<string>? Keywords { get; set; } = null!;
    public ThesisType ThesisType { get; set; }
}