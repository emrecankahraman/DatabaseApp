using System;
using System.Collections.Generic;

namespace DatabaseApp.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<CoSupervisor> CoSupervisors { get; set; } = new List<CoSupervisor>();

    public virtual ICollection<Supervisor> Supervisors { get; set; } = new List<Supervisor>();
}
