using UnityEngine;
using System.Collections;

public class HealthPotion : PickupItem
{
    [SerializeField]
    private float health = 30;

    public override void ItemPickUp(AActor actor)
    {
        owner = actor;
        UseItem(actor);
        RemoveItem();
    }

    public override void UseItem(AActor actor)
    {
        actor.CurrentHealth += health;
    }
}
