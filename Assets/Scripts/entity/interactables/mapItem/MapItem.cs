using UnityEngine;
using System.Collections;

public abstract class MapItem : AInteractable
{
    public override void Interact(AActor actor)
    {
        
    }

    public override void RemoveItem()
    {
        Destroy(gameObject);
    }

    public abstract void ItemStart();

    public abstract void ItemFinish();
}
