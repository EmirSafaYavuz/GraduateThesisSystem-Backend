﻿using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public class KeywordsThesis : IEntity
{
    public int Id { get; set; }

    public int KeywordId { get; set; }

    public int ThesisId { get; set; }
}
