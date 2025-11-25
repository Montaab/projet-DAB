namespace DAL.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public int? ProfileId { get; set; }
}

public class RoleCreateDto
{
    public string Nom { get; set; }
    public int? ProfileId { get; set; }
}

public class RoleUpdateDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public int? ProfileId { get; set; }
}
