using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string TeacherName { get; set; } = null!;

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
}
