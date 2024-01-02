namespace N90.Domain.Common.Events;

public abstract class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;

    public bool Redelivered { get; set; }
}