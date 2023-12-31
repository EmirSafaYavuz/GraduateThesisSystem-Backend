﻿using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class SubjectTopic : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
