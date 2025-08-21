namespace Backend.Dataaccess.Entities;


public class CarEntity
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int HorsePower { get; set; }
    public string Color { get; set; } = string.Empty;
    public int Price { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
}

