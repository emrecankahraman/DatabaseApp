using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class Thesis
{
    public int ThesisNumber { get; set; }

    public string? Title { get; set; }

    public string? Abstract { get; set; }

    public int? Year { get; set; }

    public string? Type { get; set; }

    public int? NumOfPages { get; set; }

    public string? Language { get; set; }

    public DateOnly? SubmissionDate { get; set; }

    public int? SupervisorId { get; set; }

    public int? CosupervisorId { get; set; }

    public int? AuthorId { get; set; }

    public int? KeywordId { get; set; }

    public int? UniversityId { get; set; }

    public int? InstituteId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual CoSupervisor? Cosupervisor { get; set; }

    public virtual Institute? Institute { get; set; }

    public virtual Keyword? Keyword { get; set; }

    public virtual Supervisor? Supervisor { get; set; }

    public virtual University? University { get; set; }

    public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();

    public virtual ICollection<SubjectTopic> Topics { get; set; } = new List<SubjectTopic>();
}
