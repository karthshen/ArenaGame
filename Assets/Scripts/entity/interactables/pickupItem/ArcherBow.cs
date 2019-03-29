using UnityEngine;
using UnityEditor;

public class ArcherBow : PickupItem
{
    GameObject archerArrow;

    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);
    }

    public override void UseItem(AActor actor)
    {
        //Shoot the arrow
        archerArrow = Object.Instantiate(Resources.Load("ArcherArrow") as GameObject);
        ArcherArrow arrow = archerArrow.GetComponent<ArcherArrow>();
        arrow.SetOwner(owner);
        arrow.ProjectileStart();
    }
}