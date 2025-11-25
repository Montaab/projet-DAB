namespace DAL.DTOs;

public class ComposantDto
{
    public int Id { get; set; }
    public string NomType { get; set; }
    public string Etat { get; set; }
    public DateOnly? Dateinstallation { get; set; }
    public int? Iddab { get; set; }
}

public class ComposantCreateDto
{
    public string NomType { get; set; }
    public string Etat { get; set; }
    public DateOnly? Dateinstallation { get; set; }
    public int? Iddab { get; set; }
}

public class ComposantUpdateDto
{
    public int Id { get; set; }
    public string NomType { get; set; }
    public string Etat { get; set; }
    public DateOnly? Dateinstallation { get; set; }
    public int? Iddab { get; set; }
}
