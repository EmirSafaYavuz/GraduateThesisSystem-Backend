using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class Language : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
