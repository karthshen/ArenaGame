using UnityEngine;
using System.Collections;

public class EnergyPotion : PickupItem
{
    [SerializeField]
    private float energy = 5;

    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);

        UseItem(actor);
        RemoveItem();
    }

    public override void UseItem(AActor actor)
    {
        actor.CurrentEnergy += energy;
    }
}
