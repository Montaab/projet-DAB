namespace DAL.DTOs;

public class MenuDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
}

public class MenuCreateDto
{
    public string Nom { get; set; }
}

public class MenuUpdateDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
}
