namespace DAL.DTOs;

public class ProfileDto
{
    public int Id { get; set; }
    public string Permission { get; set; }
}

public class ProfileCreateDto
{
    public string Permission { get; set; }
}

public class ProfileUpdateDto
{
    public int Id { get; set; }
    public string Permission { get; set; }
}
