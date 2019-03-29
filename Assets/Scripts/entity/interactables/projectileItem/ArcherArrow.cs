using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ArcherArrow : ProjectileItem
{
    private AActor owner;
    private float velocity = 15.0f;
    private float moveHorizontal = 0.1f;
    private Vector3 movement;

    private const float DURATION_TIME = 2.0f;

    private float duration_time = 0f;

    private void Start()
    {

    }

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
        Collider[] collidersToIgnore = owner.GetComponentsInChildren<Collider>();

        foreach (Collider collider in collidersToIgnore)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collider);
        }

        gameObject.transform.GetChild(0).rotation = owner.transform.GetChild(0).rotation;
        float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);
        movement = new Vector3(1 * Mathf.Sin(yDirectionInRadian), 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AttackPower / 1.5f, owner);
        }

        if (collision.gameObject.GetComponent<PickupItem>())
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            return;
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