using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ArcherArrow : ProjectileItem
{
    public GameObject com;

    private AActor owner;
    private float velocity = 20.0f;
    private float moveHorizontal = -0.06f;
    private Vector3 movement;
    private float yModifier = 0.06f;
    private float damageModifier = 1f;

    private const float DURATION_TIME = 2.0f;

    private float duration_time = 0f;

    public float YModifier
    {
        get
        {
            return yModifier;
        }

        set
        {
            yModifier = value;
        }
    }

    public float DamageModifier
    {
        get
        {
            return damageModifier;
        }

        set
        {
            damageModifier = value;
        }
    }

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = com.transform.position;
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
        //Ignore Collision of owner and this
        IgnoreOwnerCollision(owner);

        gameObject.transform.GetChild(0).rotation = owner.transform.GetChild(0).rotation;
        float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + yModifier + owner.transform.lossyScale.y / 2, owner.transform.position.z);
        movement = new Vector3(1 * Mathf.Sin(yDirectionInRadian), 0.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AttackPower / 1.5f * DamageModifier, owner);
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