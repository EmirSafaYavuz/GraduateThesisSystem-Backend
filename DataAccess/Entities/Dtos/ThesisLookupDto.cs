namespace DataAccess.Entities.Dtos;

public class ThesisLookupDto
{
    public int Id { get; set; }

    public int ThesisNo { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = null!;
    
    public string ThesisType { get; set; }
}