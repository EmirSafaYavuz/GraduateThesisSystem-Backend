using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Institute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UniversityId { get; set; }

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();

    public virtual University University { get; set; } = null!;
}
