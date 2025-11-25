using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
