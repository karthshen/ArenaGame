using UnityEngine;
using System.Collections;

public class WarriorShield : PickupItem
{
    [SerializeField]
    private float knockingForce = 200f;

    public override void UseItem(AActor actor)
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void ItemPickUp(AActor actor)
    {
        base.ItemPickUp(actor);

        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (owner.GetState().GetType() == typeof(ActorStandingState) && GetComponent<Collider>().enabled == true)
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor attackedActor = collision.gameObject.GetComponent<AActor>();
        if (attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
        {
            if (attackedActor.TakeDamage(owner.GetActorStat().AttackPower / 2, owner) != 0)
            {
                attackedActor.ClearForceOnActor();

                attackedActor.KnockBack(knockingForce, owner);

                GameObject swordblast = Object.Instantiate(Resources.Load("SwordBurst")) as GameObject;

                FireballBurst blast = swordblast.GetComponent<FireballBurst>();

                blast.Owner = owner;

                //blast.SetPositionToEnitty(this);

                blast.transform.position = collision.contacts[0].point;

                blast.ItemStart();
            }
        }
    }
}
