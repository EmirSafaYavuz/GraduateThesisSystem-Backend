using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class SubjectTopicsThesis
{
    public int Id { get; set; }

    public int SubjectTopicId { get; set; }

    public int ThesisId { get; set; }

    public virtual SubjectTopic SubjectTopic { get; set; } = null!;

    public virtual Thesis Thesis { get; set; } = null!;
}
