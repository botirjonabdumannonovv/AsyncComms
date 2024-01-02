namespace N90.Domain.Common.Entities;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}