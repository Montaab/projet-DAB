namespace DAL.DTOs;

public class CaisseDto
{
    public int Id { get; set; }
    public int? Nombrebillets { get; set; }
    public int? Valeurbillet { get; set; }
}

public class CaisseCreateDto
{
    public int Id { get; set; }
    public int? Nombrebillets { get; set; }
    public int? Valeurbillet { get; set; }
}

public class CaisseUpdateDto
{
    public int Id { get; set; }
    public int? Nombrebillets { get; set; }
    public int? Valeurbillet { get; set; }
}
