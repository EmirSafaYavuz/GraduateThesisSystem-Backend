using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using DataAccess.Entities.Enums;

namespace DataAccess.Entities;

public partial class Thesis : IEntity
{
    public int Id { get; set; }

    public int ThesisNo { get; set; }

    public string Title { get; set; } = null!;

    public string Abstract { get; set; } = null!;

    public int Year { get; set; }

    public int NumOfPages { get; set; }

    public DateTime SubmissionDate { get; set; }

    public int AuthorId { get; set; }

    public int LanguageId { get; set; }

    public int? CoSupervisorId { get; set; }

    public int InstituteId { get; set; }

    public int SupervisorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Supervisor? CoSupervisor { get; set; }

    public virtual Institute Institute { get; set; } = null!;

    public virtual ICollection<KeywordsThesis> KeywordsTheses { get; set; } = new List<KeywordsThesis>();

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<SubjectTopicsThesis> SubjectTopicsTheses { get; set; } = new List<SubjectTopicsThesis>();

    public virtual Supervisor Supervisor { get; set; } = null!;

    public virtual ICollection<SupervisorsThesis> SupervisorsTheses { get; set; } = new List<SupervisorsThesis>();
    [Column("thesis_type")]
    public virtual ThesisType ThesisType { get; set; }
}
