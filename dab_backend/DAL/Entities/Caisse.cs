using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Caisse
{
    public int Id { get; set; }

    public int? Nombrebillets { get; set; }

    public int? Valeurbillet { get; set; }

    public virtual Composant IdNavigation { get; set; }
}
