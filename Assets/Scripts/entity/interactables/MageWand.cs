﻿using UnityEngine;
using System.Collections;

public class MageWand : PickupItem
{
    public override void ItemPickUp(AActor actor)
    {
        this.owner = actor;
    }

    public override void UseItem()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void Update()
    {
        if (owner.GetState().GetType() != typeof(ActorAttackState))
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (owner.GetState().GetType() == typeof(ActorAttackState))
        {
            AActor attackedActor = collision.gameObject.GetComponent<AActor>();
            if (attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
            {
                attackedActor.TakeDamage(owner.GetActorStat().AttackPower);
            }
        }
    }
}
