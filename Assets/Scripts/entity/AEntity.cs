﻿using UnityEngine;

public abstract class AEntity
{
    protected System.Guid entityId;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;

    protected AEntity()
    {
       
    }

    public System.Guid GetEntityId()
    {
        return entityId;
    }

    public string GetName()
    {
        return entityName;
    }

    public EntityState GetState()
    {
        return state;
    }

    public Mesh GetMesh()
    {
        return entityMesh;
    }

    public void NullParameterCheck()
    {
         if (entityId == null || entityName == null || state == null || entityMesh == null)
        {
            throw new UnassignedReferenceException(GetType() + "class is not initialized correctly");
        }
    }
}
