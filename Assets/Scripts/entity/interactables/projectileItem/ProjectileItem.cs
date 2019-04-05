using UnityEngine;
using UnityEditor;

public abstract class ProjectileItem : AInteractable
{
    public override void Interact(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveItem()
    {
        Destroy(this.gameObject);
    }

    public abstract void ProjectileStart();

    public abstract void ProjectileFinish();

    protected void IgnoreOwnerCollision(AActor owner)
    {
        Collider[] collidersToIgnore = owner.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), collider);
        }

        Collider ownerCollider = owner.GetComponent<Collider>();
        Physics.IgnoreCollision(GetComponentInChildren<Collider>(), ownerCollider);
    }
}