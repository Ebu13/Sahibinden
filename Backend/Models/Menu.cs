using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public string? Amblem { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Home> Homes { get; set; } = new List<Home>();

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Menu? Parent { get; set; }
}
