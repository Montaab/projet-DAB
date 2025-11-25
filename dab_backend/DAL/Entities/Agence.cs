using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Agence
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Adresse { get; set; }

    public virtual ICollection<Dab> Dabs { get; set; } = new List<Dab>();
}
