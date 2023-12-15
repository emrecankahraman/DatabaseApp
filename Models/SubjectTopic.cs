using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class SubjectTopic
{
    public int TopicId { get; set; }

    public string? TopicName { get; set; }

    public virtual ICollection<Thesis> ThesisNumbers { get; set; } = new List<Thesis>();
}
