using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class SupervisorsThesis : IEntity
{
    public int Id { get; set; }

    public int SupervisorId { get; set; }

    public int ThesisId { get; set; }
}
