using UnityEngine;
using System.Collections;

public class WarriorSword : PickupItem
{
    public override void ItemPickUp(AActor actor)
    {
        this.owner = actor;
    }

    public override void UseItem()
    {
        Debug.Log("Warrior waves sowrd");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(owner.GetState().GetType() == typeof(ActorAttackState))
        {
            AActor attackedActor = collision.gameObject.GetComponent<AActor>();
            if(attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
            {
                attackedActor.TakeDamage(owner.GetActorStat().AttackPower);
            }
        }
    }
}
