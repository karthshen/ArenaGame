using UnityEngine;
using System.Collections;

public class UpwardCalibur : ProjectileItem
{
    [SerializeField]
    private float velocity = 20.0f;
    private float moveHorizontal = 0.5f;
    private Vector3 movement;

    private const float DURATION_TIME = 0.9f;

    private float duration_time = 0f;

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    public override void ProjectileStart()
    {
        //Ignore this collision and owner component collision
        IgnoreOwnerCollision(owner);

        SetRotationToEntity(owner);
        float yDirectionInRadian = GetYDirectionInRadian();

        if (owner.IsGrounded)
        {
            gameObject.transform.position = new Vector3(owner.transform.position.x,
                owner.transform.position.y + owner.transform.lossyScale.y /2f, owner.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(owner.transform.position.x,
               owner.transform.position.y + owner.transform.lossyScale.y/2 * 0.5f, owner.transform.position.z);
        }
        movement = new Vector3(0f, moveHorizontal, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AttackPower * 1f, owner);
            IgnoreEntityCollision(hitActor);

            //GameObject swordblast = Object.Instantiate(Resources.Load("SwordBurst")) as GameObject;

            //FireballBurst blast = swordblast.GetComponent<FireballBurst>();

            //blast.Owner = owner;

            ////blast.SetPositionToEnitty(this);

            //blast.transform.position = new Vector3(hitActor.transform.position.x,
            //   hitActor.transform.position.y + owner.transform.lossyScale.y / 2, hitActor.transform.position.z);

            //blast.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            //blast.ItemStart();

            return;
        }

        if (!collision.gameObject.GetComponent<AEntity>())
        {
            IgnoreGameobjectCollision(collision.gameObject);
            return;
        }

        if (collision.gameObject.GetComponent<PickupItem>())
        {
            IgnoreEntityCollision(collision.gameObject.GetComponent<PickupItem>());
            return;
        }

        //ProjectileFinish();
    }

    private void Update()
    {
        transform.Translate(movement * velocity * Time.deltaTime);
        duration_time += Time.deltaTime;

        if (duration_time >= DURATION_TIME)
            ProjectileFinish();
    }
}
