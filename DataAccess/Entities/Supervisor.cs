using System;
using System.Collections.Generic;
using Core.Entities;

namespace DataAccess.Entities;

public partial class Supervisor : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<SupervisorsThesis> SupervisorsTheses { get; set; } = new List<SupervisorsThesis>();

    public virtual ICollection<Thesis> ThesisCoSupervisors { get; set; } = new List<Thesis>();

    public virtual ICollection<Thesis> ThesisSupervisors { get; set; } = new List<Thesis>();
}
