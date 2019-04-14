using UnityEngine;
using System.Collections;

public class WarriorSword : PickupItem
{
    public GameObject com;

    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);
        IgnoreOwnerCollision(owner);
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
        if (attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
        {
            if (attackedActor.TakeDamage(owner.GetActorStat().AttackPower, owner) != 0)
            {

                GameObject swordblast = Object.Instantiate(Resources.Load("SwordBurst")) as GameObject;

                FireballBurst blast = swordblast.GetComponent<FireballBurst>();

                blast.Owner = owner;

                //blast.SetPositionToEnitty(this);

                blast.transform.position = com.transform.position;

                blast.ItemStart();
            }
        }
    }
}
