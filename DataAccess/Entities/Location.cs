using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class Location : IEntity
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
