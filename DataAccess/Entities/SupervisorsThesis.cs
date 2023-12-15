using System;
using System.Collections.Generic;

namespace DataAccess.Entities;

public partial class SupervisorsThesis
{
    public int Id { get; set; }

    public int SupervisorId { get; set; }

    public int ThesisId { get; set; }

    public virtual Supervisor Supervisor { get; set; } = null!;

    public virtual Thesis Thesis { get; set; } = null!;
}
