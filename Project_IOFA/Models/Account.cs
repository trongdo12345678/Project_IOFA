using System;
using System.Collections.Generic;

namespace ProjectSem3.Models;

public partial class Account
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
