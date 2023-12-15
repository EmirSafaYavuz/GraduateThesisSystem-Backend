﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class Language
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}