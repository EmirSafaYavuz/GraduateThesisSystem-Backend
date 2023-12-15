using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class SubjectTopic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SubjectTopicsThesis> SubjectTopicsTheses { get; set; } = new List<SubjectTopicsThesis>();
}
