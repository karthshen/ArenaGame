using UnityEngine;
using System.Collections;

public class StormShield : MapItem
{
    private float duration_time = 0f;
    private float knockingForce = 3.5f;
    private AActor owner;

    private const float DURATION_TIME = 1.0f;

    public AActor Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    public override void ItemStart()
    { 
        gameObject.transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y, 0);
        ParticleSystem ps = gameObject.GetComponentInChildren<ParticleSystem>();

        if (ps)
        {
            ps.playbackSpeed = 4;
        }
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void KnockBackCollidedActor(Collider other)
    {
        AActor attackedActor = other.GetComponentInParent<AActor>();
        if (attackedActor && attackedActor.GetEntityId() != owner.GetEntityId())
        {
            attackedActor.TakeDamage(owner.GetActorStat().AbiltiyPower / 2, owner);
            attackedActor.KnockBack(knockingForce, owner);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        KnockBackCollidedActor(other);
    }

    private void OnTriggerStay(Collider other)
    {
        KnockBackCollidedActor(other);
    }


    private void Update()
    {
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ItemFinish();

        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider.size.x < 5f)
        {
            collider.size = new Vector3(collider.size.x + Time.deltaTime * 100, collider.size.y, collider.size.z);
        }
    }
}
