using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class KeywordsThesis
{
    public int Id { get; set; }

    public int KeywordId { get; set; }

    public int ThesisId { get; set; }

    public virtual Keyword Keyword { get; set; } = null!;

    public virtual Thesis Thesis { get; set; } = null!;
}
