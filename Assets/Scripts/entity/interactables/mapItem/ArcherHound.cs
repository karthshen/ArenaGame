using UnityEngine;
using System.Collections;

public class ArcherHound : MapItem
{
    enum HoundAnimation
    {
        Bite = 1,
        Move = 2
    }
    private const float MOVE_VELOCITY = 20.0f;

    private AActor owner;
    private float horizontal = 0.3f;
    private float disappearTime = 10.0f;
    private Animator animator;
    private float throwForce = 100f;
    private bool isGrounded = false;

    private float currentVelocity = 0f;
    private HoundAnimation currentAnim;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentAnim == HoundAnimation.Move)
        {
            Move();
        }

        disappearTime -= Time.deltaTime;
        if(disappearTime <= 0)
        {
            ItemFinish();
        }
    }

    public override void ItemStart()
    {
        IgnoreOwnerCollision(owner);
        animator = GetComponent<Animator>();

        gameObject.transform.GetChild(0).rotation = owner.transform.GetChild(0).rotation;

        Rigidbody rb = GetComponent<Rigidbody>();

        float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        gameObject.transform.position = new Vector3(owner.transform.position.x + horizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

        rb.AddForce(new Vector3(throwForce * Mathf.Sin(yDirectionInRadian), throwForce, 0));

        currentVelocity = MOVE_VELOCITY * Mathf.Sin(yDirectionInRadian);

        PlayAnimation(HoundAnimation.Move);
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void PlayAnimation(HoundAnimation anim)
    {
        animator.SetInteger("animation", (int)anim);
        currentAnim = anim;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "InvisibleTriggerWall")
        {
            currentVelocity = -currentVelocity;
            TurnAround();
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(currentVelocity, 0f, 0f);
        transform.Translate(movement * Time.deltaTime);
    }

    private void TurnAround()
    {
        if(transform.GetChild(0).eulerAngles == AEntity.FRONT_DIRECTION)
        {
            transform.GetChild(0).eulerAngles = AEntity.BACK_DIRECTION;
        }
        else if(transform.GetChild(0).eulerAngles == AEntity.BACK_DIRECTION)
        {
            transform.GetChild(0).eulerAngles = AEntity.FRONT_DIRECTION;
        }
    }
}
