using UnityEngine;
using System.Collections;

public class Thunderstrike : MapItem
{
    public float disappearTime = 1.5f;

    private AActor owner;

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
        IgnoreOwnerCollision(owner);

        gameObject.transform.position = new Vector3(owner.transform.position.x,
            owner.transform.position.y, owner.transform.position.z);
    }

    public override void Interact(AActor actor)
    {
        base.Interact(actor);

        actor.TakeDamageAndFreeze(owner.GetActorStat().AbiltiyPower,0.5f, owner);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor actor = collision.gameObject.GetComponent<AActor>();
        if (actor)
        {
            Interact(actor);
        }
    }

    private void Update()
    {
        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            ItemFinish();
        }
    }
}
