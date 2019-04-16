using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    protected System.Guid entityId;
    protected string entityName;
    protected EntityState state;
    protected Mesh entityMesh;
    protected AudioSource effectSource;

    public static Vector3 FRONT_DIRECTION = new Vector3(0, 90, 0);
    public static Vector3 BACK_DIRECTION = new Vector3(0, 270, 0);

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

    public AudioSource GetAudioSource()
    {
        return effectSource;
    }

    public void NullParameterCheck()
    {
        if (entityId == null || entityName == null || state == null )//|| entityMesh == null)
        {
            throw new UnassignedReferenceException(GetType() + "class is not initialized correctly");
        }
    }

    protected void SetRotationToEntity(AEntity entity)
    {
        gameObject.transform.GetChild(0).rotation = entity.transform.GetChild(0).rotation;
    }

    public float GetYDirectionInRadian()
    {
        return transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;
    }

    public void SetPositionToEnitty(AEntity entity)
    {
        gameObject.transform.position = entity.transform.position;
    }
    protected void IgnoreOwnerCollision(AActor owner)
    {
        if (!owner)
            return;

        Collider[] collidersToIgnore = owner.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach (Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = owner.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        foreach (Collider selfCollider in colliderInSelf)
        {
            Physics.IgnoreCollision(selfCollider, ownerCollider);
        }
    }

    protected void IgnoreEntityCollision(AEntity entity)
    {
        if (!entity)
            return;

        Collider[] collidersToIgnore = entity.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach (Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = entity.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        if (ownerCollider)
        {
            foreach (Collider selfCollider in colliderInSelf)
            {
                Physics.IgnoreCollision(selfCollider, ownerCollider);
            }
        }
    }

    protected void IgnoreGameobjectCollision(GameObject gameObject)
    {
        if (!gameObject)
            return;

        Collider[] collidersToIgnore = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Collider[] collidersInSelf = GetComponentsInChildren<Collider>();
            foreach (Collider selfCollider in collidersInSelf)
            {
                Physics.IgnoreCollision(selfCollider, collider);
            }
        }

        Collider ownerCollider = gameObject.GetComponent<Collider>();
        Collider[] colliderInSelf = GetComponentsInChildren<Collider>();
        if (ownerCollider)
        {
            foreach (Collider selfCollider in colliderInSelf)
            {
                Physics.IgnoreCollision(selfCollider, ownerCollider);
            }
        }
    }
}
