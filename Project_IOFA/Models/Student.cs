using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Email { get; set; }

    public string? StudentName { get; set; }

    public string? Password { get; set; }

    public int? ClassId { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

    public virtual Class? Class { get; set; }
}
