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
}