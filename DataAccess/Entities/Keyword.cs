using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Keyword
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<KeywordsThesis> KeywordsTheses { get; set; } = new List<KeywordsThesis>();
}
