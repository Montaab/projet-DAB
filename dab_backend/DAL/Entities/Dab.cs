using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Dab
{
    public int Id { get; set; }

    public string Statut { get; set; }

    public double? Montantdisponible { get; set; }

    public string Localisation { get; set; }

    public int? Idagence { get; set; }

    public virtual ICollection<Composant> Composants { get; set; } = new List<Composant>();

    public virtual Agence IdagenceNavigation { get; set; }
}
