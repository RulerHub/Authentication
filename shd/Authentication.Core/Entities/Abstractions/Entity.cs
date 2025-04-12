namespace Authentication.Core.Entities.Abstractions;

public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
}
