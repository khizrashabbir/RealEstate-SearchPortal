namespace Domain.Entities;

public class Favorite
{
    public long UserId { get; set; }
    public long PropertyId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
