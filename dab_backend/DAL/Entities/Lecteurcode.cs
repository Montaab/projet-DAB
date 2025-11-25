using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Lecteurcode
{
    public int Id { get; set; }

    public string Typelecteur { get; set; }

    public virtual Composant IdNavigation { get; set; }
}
