using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected int entityid;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;

    public int GetEntityId()
    {
        return this.entityid;
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
