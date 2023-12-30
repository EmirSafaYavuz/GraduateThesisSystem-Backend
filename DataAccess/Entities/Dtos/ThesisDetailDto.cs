using Core.Entities;
using DataAccess.Entities.Enums;

namespace DataAccess.Entities.Dtos;

public class ThesisDetailDto : IDto
{
    public int Id { get; set; }

    public int ThesisNo { get; set; }

    public string Title { get; set; } = null!;

    public string Abstract { get; set; } = null!;

    public int Year { get; set; }

    public int NumOfPages { get; set; }

    public DateTime SubmissionDate { get; set; }

    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;

    public int LanguageId { get; set; }
    public string LanguageName { get; set; } = null!;

    public int? CoSupervisorId { get; set; }
    public string? CoSupervisorName { get; set; }

    public int InstituteId { get; set; }
    public string InstituteName { get; set; } = null!;

    public int SupervisorId { get; set; }
    public string SupervisorName { get; set; } = null!;
    public string ThesisType { get; set; }
}