using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public int? ProfileId { get; set; }

    public virtual Profile Profile { get; set; }

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
