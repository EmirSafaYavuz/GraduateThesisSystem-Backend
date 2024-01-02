using System;
using System.Collections.Generic;
using Core.Entities;
using Newtonsoft.Json;

namespace DataAccess.Entities;

public class Author : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
