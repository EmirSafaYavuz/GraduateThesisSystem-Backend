using Core.Entities;
using Entities.Enum;

namespace Entities.Concrete;

public class Thesis : IEntity
{
    public int Id { get; set; }
    public int ThesisNo { get; set; }
    public string Title { get; set; }
    public string Abstract { get; set; }
    public int Year { get; set; }
    public int NumberOfPages { get; set; }
    public DateTime SubmissionDate { get; set; }
    public int AuthorId { get; set; }
    public int LanguageId { get; set; }
    public int SupervisorId { get; set; }
    public int CoSupervisorId { get; set; }
    public int InstituteId { get; set; }
    public ThesisType ThesisType { get; set; }
}