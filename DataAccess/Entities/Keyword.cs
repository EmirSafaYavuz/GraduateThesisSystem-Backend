using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class Keyword : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
