﻿using UnityEngine;
using System.Collections;

public class MageWand : PickupItem
{
    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);
    }

    public override void UseItem(AActor actor)
    {
        GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        if (owner.GetState().GetType() != typeof(ActorAttackState) && owner.GetState().GetType() != typeof(ActorAirAttackState))
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor attackedActor = collision.gameObject.GetComponent<AActor>();
        if ((attackedActor && owner) && attackedActor.GetEntityId() != owner.GetEntityId())
        {
            attackedActor.TakeDamage(owner.GetActorStat().AttackPower, owner);
        }
    }
}
