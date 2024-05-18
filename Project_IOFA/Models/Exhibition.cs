using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Exhibition
{
    public int ExhibitionId { get; set; }

    public string? ExhibitionName { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
