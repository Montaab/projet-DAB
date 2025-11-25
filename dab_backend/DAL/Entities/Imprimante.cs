using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Imprimante
{
    public int Id { get; set; }

    public int? Niveaupapier { get; set; }

    public virtual Composant IdNavigation { get; set; }
}
