using UnityEngine;
using UnityEditor;

public class ArcherBow : PickupItem
{
    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);
    }

    public override void UseItem(AActor actor)
    {
        //Shoot the arrow
    }
}