using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class University
{
    public int UniversityId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Institute> Institutes { get; set; } = new List<Institute>();

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
