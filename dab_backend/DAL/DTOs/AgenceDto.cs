namespace DAL.DTOs;

public class AgenceDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
}

public class AgenceCreateDto
{
    public string Nom { get; set; }
    public string Adresse { get; set; }
}

public class AgenceUpdateDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Adresse { get; set; }
}
