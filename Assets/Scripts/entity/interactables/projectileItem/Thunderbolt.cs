using UnityEngine;
using System.Collections;

public class Thunderbolt : ProjectileItem
{
    private AActor owner;
    private float velocity = 5.0f;
    private float moveHorizontal = 1.0f;
    private Vector3 movement;

    public void SetOwner(AActor owner)
    {
        this.owner = owner;
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(transform.rotation.eulerAngles.y),
            owner.transform.position.y, owner.transform.position.z);

        gameObject.transform.rotation = owner.transform.rotation;
        movement = new Vector3(moveHorizontal * Mathf.Sin(transform.rotation.eulerAngles.y), 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AbiltiyPower);
        }

        ProjectileFinish();
    }

    private void Update()
    {
        transform.Translate(movement * velocity * Time.deltaTime);
    }
}
