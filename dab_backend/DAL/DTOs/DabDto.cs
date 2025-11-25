namespace DAL.DTOs;

public class DabDto
{
    public int Id { get; set; }
    public string Statut { get; set; }
    public double? Montantdisponible { get; set; }
    public string Localisation { get; set; }
    public int? Idagence { get; set; }
}

public class DabCreateDto
{
    public string Statut { get; set; }
    public double? Montantdisponible { get; set; }
    public string Localisation { get; set; }
    public int? Idagence { get; set; }
}

public class DabUpdateDto
{
    public int Id { get; set; }
    public string Statut { get; set; }
    public double? Montantdisponible { get; set; }
    public string Localisation { get; set; }
    public int? Idagence { get; set; }
}
