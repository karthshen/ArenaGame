using UnityEngine;
using System.Collections;

public abstract class PickupItem : AInteractable
{
    protected AActor owner;

    public override void Interact(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveItem()
    {
        Destroy(gameObject);
    }

    public abstract void UseItem(AActor actor);

    public virtual void ItemPickUp(AActor actor)
    {
        if (owner != null)
            return;
        else
            owner = actor;
    }

    public AActor GetOwner()
    {
        return owner;
    }
}
