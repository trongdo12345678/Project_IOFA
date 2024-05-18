using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectSem3.Models;

public partial class Artwork
{
    public int ArtworkId { get; set; }
    [Required]
    public string? ArtworkName { get; set; }

    public int? StudentId { get; set; }
    [Required]
    public string? FileUrl { get; set; }

    public string? Status { get; set; }
    [Required]
    public string? Descritption { get; set; }

    public DateTime? DayPost { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
