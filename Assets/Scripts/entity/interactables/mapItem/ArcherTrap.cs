using UnityEngine;
using System.Collections;

public class ArcherTrap : MapItem
{
    private float damage = 5f;
    private AActor owner;
    private Animator animator;
    private float freezeTime = 1f;
    private float moveForce = 300f;
    private bool closed = false;

    private float moveHorizontal = 0.3f;

    private float disappearTime = 15.0f;

    private bool isGrounded = false;

    enum TrapAnimation
    {
        Open = 1,
        Close = 2
    }

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

    private void Start()
    {

    }

    public override void Interact(AActor actor)
    {
        if (!closed && isGrounded)
        {
            base.Interact(actor);
            PlayAnimation(TrapAnimation.Close);
            owner.AttackCode = System.Guid.NewGuid();
            actor.TakeDamageAndFreeze(damage, freezeTime, owner);
            closed = true;
            disappearTime = 1.0f;
        }
    }

    public override void ItemStart()
    {
        IgnoreOwnerCollision(owner);

        animator = GetComponent<Animator>();

        gameObject.transform.GetChild(0).rotation = owner.transform.GetChild(0).rotation;

        Rigidbody rb = GetComponent<Rigidbody>();

        float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        gameObject.transform.position = new Vector3(owner.transform.position.x + moveHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

        rb.AddForce(new Vector3(moveForce * Mathf.Sin(yDirectionInRadian), moveForce, 0));

        PlayAnimation(TrapAnimation.Open);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void PlayAnimation(TrapAnimation anim)
    {
        animator.SetInteger("animation", (int)anim);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor actor = collision.gameObject.GetComponent<AActor>();

        if (actor && owner && owner.GetEntityId() != actor.GetEntityId())
        {
            Interact(actor);
        }

        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void Update()
    {

        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0 && !closed)
        {
            PlayAnimation(TrapAnimation.Close);
            closed = true;
            disappearTime = 1;
        }
        else if (disappearTime<=0 && closed)
        {
            ItemFinish();
        }
    }
}
