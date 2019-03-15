using UnityEngine;
using System.Collections;

public class Thunderbolt : ProjectileItem
{
    private AActor owner;
    private float velocity = 10.0f;
    private float moveHorizontal = 1.0f;
    private Vector3 movement;

    private const float DURATION_TIME = 2.0f;

    private float duration_time = 0f;

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
        gameObject.transform.GetChild(0).rotation = owner.transform.GetChild(0).rotation;
        float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y/2, owner.transform.position.z);
        movement = new Vector3(moveHorizontal * Mathf.Sin(yDirectionInRadian), 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AbiltiyPower / 3);
        }

        ProjectileFinish();
    }

    private void Update()
    {
        transform.Translate(movement * velocity * Time.deltaTime);
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ProjectileFinish();
    }
}
