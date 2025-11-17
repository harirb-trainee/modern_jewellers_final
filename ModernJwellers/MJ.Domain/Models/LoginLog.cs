using System;
using System.Collections.Generic;

namespace MJ;

public partial class LoginLog
{
    public int? UserId { get; set; }

    public DateTime? LoginAt { get; set; }

    public int Id { get; set; }

    public virtual User? User { get; set; }
}
