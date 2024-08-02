using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public string ProductType { get; set; } = null!;

    public int MenuId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
