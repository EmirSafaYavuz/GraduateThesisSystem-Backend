using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class SubjectTopicsThesis : IEntity
{
    public int Id { get; set; }

    public int SubjectTopicId { get; set; }

    public int ThesisId { get; set; }

    public virtual SubjectTopic SubjectTopic { get; set; } = null!;

    public virtual Thesis Thesis { get; set; } = null!;
}
