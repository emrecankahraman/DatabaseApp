using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? Name { get; set; }

    public int? PersonId { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
