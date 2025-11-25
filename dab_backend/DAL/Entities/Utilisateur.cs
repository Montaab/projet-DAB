using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Email { get; set; }

    public string MotDePasse { get; set; }

    public int? RoleId { get; set; }

    public virtual Role Role { get; set; }
}
