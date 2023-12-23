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

    public int? CoSupervisorId { get; set; }

    public int? AuthorId { get; set; }

    public int? KeywordId { get; set; }

    public int? TopicId { get; set; }

    public int? UniversityId { get; set; }

    public int? InstituteId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual CoSupervisor? CoSupervisor { get; set; }

    public virtual Institute? Institute { get; set; }

    public virtual Keyword? Keyword { get; set; }

    public virtual Supervisor? Supervisor { get; set; }

    public virtual SubjectTopic? Topic { get; set; }

    public virtual University? University { get; set; }
}
