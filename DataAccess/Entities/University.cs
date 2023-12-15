using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class University
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int LocationId { get; set; }

    public virtual ICollection<Institute> Institutes { get; set; } = new List<Institute>();

    public virtual Location Location { get; set; } = null!;
}
