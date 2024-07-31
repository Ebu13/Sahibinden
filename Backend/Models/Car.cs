using System;
using System.Collections.Generic;

namespace Backend.Models
{

    public partial class Car
    {
        public int CarId { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public virtual Menu Menu { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}