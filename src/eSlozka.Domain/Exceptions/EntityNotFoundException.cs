namespace eSlozka.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(int entityID, string entityType)
    {
        EntityID = entityID;
        EntityType = entityType;
    }

    public int EntityID { get; }
    public string EntityType { get; }
}
