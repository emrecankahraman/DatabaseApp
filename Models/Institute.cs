using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class Institute
{
    public int InstituteId { get; set; }

    public string? Name { get; set; }

    public int? UniversityId { get; set; }

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();

    public virtual University? University { get; set; }
}
