using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class Keyword
{
    public int KeywordId { get; set; }

    public string? KeywordText { get; set; }

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
