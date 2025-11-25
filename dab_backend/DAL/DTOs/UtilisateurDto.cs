namespace DAL.DTOs;

public class UtilisateurDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public int? RoleId { get; set; }
}

public class UtilisateurCreateDto
{
    public string Nom { get; set; }
    public string Email { get; set; }
    public string MotDePasse { get; set; }
    public int? RoleId { get; set; }
}

public class UtilisateurUpdateDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public int? RoleId { get; set; }
}
