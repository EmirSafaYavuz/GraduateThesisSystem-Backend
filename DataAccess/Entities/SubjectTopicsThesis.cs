using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public class SubjectTopicsThesis : IEntity
{
    public int Id { get; set; }

    public int SubjectTopicId { get; set; }

    public int ThesisId { get; set; }
}
