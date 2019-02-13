using UnityEngine;
using System.Collections;

public abstract class PickupItem : AInteractable
{
    protected AActor owner;

    public override void InitializeItem()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact(AActor actor)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveItem()
    {
        throw new System.NotImplementedException();
    }

    public abstract void UseItem();

    public abstract void ItemPickUp(AActor actor);

    public AActor GetOwner()
    {
        return owner;
    }
}
