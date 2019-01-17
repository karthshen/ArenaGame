using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected System.Guid entityId;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;

    protected AEntity()
    {
        if(entityId.Equals(0)|| entityName.Equals("") || state.Equals(null) || entityMesh)
        {
            throw new UnassignedReferenceException(this.GetType() + "class is not initialized correctly");
        }
    }

    public System.Guid GetEntityId()
    {
        return this.entityId;
    }

    public string GetName()
    {
        return this.name;
    } 

    public EntityState GetState()
    {
        return this.state;
    }

    public Mesh GetMesh()
    {
        return this.entityMesh;
    }
}
