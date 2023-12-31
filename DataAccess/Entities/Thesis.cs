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
    [Column("thesis_type")]
    public virtual ThesisType ThesisType { get; set; }
}
