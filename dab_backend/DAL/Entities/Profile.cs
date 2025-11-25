using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Profile
{
    public int Id { get; set; }

    public string Permission { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
