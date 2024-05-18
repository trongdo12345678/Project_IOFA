using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Competition
{
    public int CompetitionId { get; set; }

    public string? CompetitionName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Img { get; set; }

    public string? Description { get; set; }

    public string? Awards { get; set; }

    public int? TeacherId { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual Teacher? Teacher { get; set; }
}
