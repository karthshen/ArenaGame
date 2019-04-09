using UnityEngine;
using UnityEditor;

public abstract class ProjectileItem : AInteractable
{
    protected AActor owner;

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

    public void SetOwner(AActor owner)
    {
        this.owner = owner;
    }

    public AActor GetOwner()
    {
        return owner;
    }
}