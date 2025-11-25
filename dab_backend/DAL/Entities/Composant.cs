using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Composant
{
    public int Id { get; set; }

    public DateOnly? Dateinstallation { get; set; }

    public string Etat { get; set; }

    public int? Iddab { get; set; }

    public string NomType { get; set; }

    public virtual Caisse Caisse { get; set; }

    public virtual Dab IddabNavigation { get; set; }

    public virtual Imprimante Imprimante { get; set; }

    public virtual Lecteurcode Lecteurcode { get; set; }
}
