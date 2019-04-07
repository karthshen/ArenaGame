﻿using UnityEngine;
using System.Collections;

public class ArcherHound : MapItem
{
    enum HoundAnimation
    {
        Bite = 2,
        Move = 1
    }
    private const float MOVE_VELOCITY = 7.0f;
    private const float FREEZE_TIME = 1.0f;

    private Animator animator;
    private AActor owner;
    private float horizontal = 0.3f;
    [SerializeField]
    private float disappearTime = 10.0f;
    [SerializeField]
    private float throwForce = 7f;
    [SerializeField]
    private float currentVelocity = 0f;
    [SerializeField]
    private int attackTimes = 3;

    private float freezeTime = 0f;

    private bool isGrounded = false;

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

    public override void ItemStart()
    {
        IgnoreOwnerCollision(owner);
        animator = GetComponentInChildren<Animator>();

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

    // Update is called once per frame
    void Update()
    {
        if (currentAnim == HoundAnimation.Move)
        {
            Move();
        }

        disappearTime -= Time.deltaTime;
        if (disappearTime <= 0)
        {
            ItemFinish();
        }

        if(freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
            if (freezeTime <= 0)
            {
                PlayAnimation(HoundAnimation.Move);
                float yDirectionInRadian = transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;
                currentVelocity = MOVE_VELOCITY * Mathf.Sin(yDirectionInRadian);
            }
        }

        FallOutPlatformCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "InvisibleTriggerWall" && isGrounded)
        {
            currentVelocity = -currentVelocity;
            TurnAround();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        //Complicated? attack logic
        AActor hitActor = collision.gameObject.GetComponent<AActor>();
        if (hitActor && attackTimes > 0)
        {
            owner.AttackCode = System.Guid.NewGuid();
            hitActor.TakeDamage(owner.GetActorStat().AttackPower, owner);
            PlayAnimation(HoundAnimation.Bite);
            currentVelocity = 0f;
            freezeTime = FREEZE_TIME;
            if(disappearTime < FREEZE_TIME)
            {
                disappearTime = FREEZE_TIME;
            }

            attackTimes--;

            if(attackTimes == 0)
            {
                disappearTime = 1f;
            }
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(currentVelocity, 0f, 0f);
        transform.Translate(movement * Time.deltaTime);
    }

    private void TurnAround()
    {
        if(currentVelocity > 0)
        {
            transform.GetChild(0).eulerAngles = AEntity.FRONT_DIRECTION;
        }
        else if(currentVelocity < 0)
        {
            transform.GetChild(0).eulerAngles = AEntity.BACK_DIRECTION;
        }
    }
}
