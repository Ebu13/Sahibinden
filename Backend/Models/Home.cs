using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Home
{
    public int HomeId { get; set; }

    public int UserId { get; set; }

    public int MenuId { get; set; }

    public string Location { get; set; } = null!;

    public int Size { get; set; }

    public decimal Price { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
