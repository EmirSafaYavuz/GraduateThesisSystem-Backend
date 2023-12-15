using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
