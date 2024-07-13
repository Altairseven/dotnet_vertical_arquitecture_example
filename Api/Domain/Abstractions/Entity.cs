namespace Api.Domain.Abstractions;

public abstract class Entity<TEntityId>
{
    protected Entity(TEntityId id) => Id = id;
    protected Entity()
    {

    }
    public TEntityId Id { get; init; }
}
