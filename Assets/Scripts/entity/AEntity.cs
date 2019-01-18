using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected System.Guid entityId;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;

    protected AEntity()
    {
        if (entityId.Equals(0) || entityName.Equals("") || state.Equals(null) || entityMesh)
        {
            throw new UnassignedReferenceException(GetType() + "class is not initialized correctly");
        }
    }

    public System.Guid GetEntityId()
    {
        return entityId;
    }

    public string GetName()
    {
        return name;
    }

    public EntityState GetState()
    {
        return state;
    }

    public Mesh GetMesh()
    {
        return entityMesh;
    }
}
