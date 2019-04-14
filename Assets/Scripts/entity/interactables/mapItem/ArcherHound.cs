using UnityEngine;
using System.Collections;

public class ArcherHound : MapItem
{
    public bool ChickenGod = false;

    enum HoundAnimation
    {
        Bite = 4,
        Move = 1
    }
    private const float MOVE_VELOCITY = 7.0f;
    private const float FREEZE_TIME = 1.5f;

    private Animator animator;
    private AudioSource audioSource;
    private AActor owner;
    private float horizontal = 0.3f;
    [SerializeField]
    private float disappearTime = 10.0f;
    [SerializeField]
    private float throwForce = 7f;
    [SerializeField]
    private float currentVelocity = 0f;
    [SerializeField]
    private int attackTimes = 2;

    private float freezeTime = 0f;

    private bool hasPlayed = false;

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
        if (ChickenGod)
        {
            float yDirectionInRadian = GetYDirectionInRadian();

            Rigidbody rb = GetComponent<Rigidbody>();

            audioSource = GetComponent<AudioSource>();

            animator = GetComponentInChildren<Animator>();

            currentVelocity = MOVE_VELOCITY * Mathf.Sin(yDirectionInRadian);

            PlayAnimation(HoundAnimation.Move);

            IgnoreCollisionWithOtherHounds();
        }
    }

    public override void ItemStart()
    {
        IgnoreOwnerCollision(owner);
        animator = GetComponentInChildren<Animator>();

        SetRotationToEntity(owner);

        Rigidbody rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        float yDirectionInRadian = GetYDirectionInRadian();

        gameObject.transform.position = new Vector3(owner.transform.position.x + horizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

        rb.AddForce(new Vector3(throwForce * Mathf.Sin(yDirectionInRadian), throwForce, 0));

        currentVelocity = MOVE_VELOCITY * Mathf.Sin(yDirectionInRadian);

        PlayAnimation(HoundAnimation.Move);
        //SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.chicken1, ref hasPlayed);

        IgnoreCollisionWithOtherHounds();
    }

    private void IgnoreCollisionWithOtherHounds()
    {
        ArcherHound[] otherChickens = GameObject.FindObjectsOfType<ArcherHound>();

        foreach (ArcherHound hound in otherChickens)
        {
            if (hound.GetInstanceID() != GetInstanceID())
            {
                IgnoreEntityCollision(hound);
            }
        }
    }

    public override void ItemFinish()
    {
        RemoveItem();
    }

    private void PlayAnimation(HoundAnimation anim)
    {
        animator.SetInteger("animation", (int)anim);

        if (anim == HoundAnimation.Move)
        {
            hasPlayed = true;
        }
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

        if (freezeTime > 0)
        {
            freezeTime -= Time.deltaTime;
            if (freezeTime <= 0)
            {
                PlayAnimation(HoundAnimation.Move);
                float yDirectionInRadian = GetYDirectionInRadian();
                currentVelocity = MOVE_VELOCITY * Mathf.Sin(yDirectionInRadian);
            }
        }

        FallOutPlatformCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InvisibleTriggerWall" && isGrounded)
        {
            currentVelocity = -currentVelocity;
            TurnAround();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        //Complicated? attack logic
        AActor hitActor = collision.gameObject.GetComponent<AActor>();
        if (hitActor && attackTimes > 0 && freezeTime <= 0)
        {
            float isHit = 0;

            if (!ChickenGod)
            {
                owner.AttackCode = System.Guid.NewGuid();
                isHit = hitActor.TakeDamage(owner.GetActorStat().AttackPower, owner);
            }
            else if (ChickenGod)
            {
                isHit = hitActor.TakeDamageFromEntity(10f, 500f, this);
            }

            PlayAnimation(HoundAnimation.Bite);

            if (currentAnim == HoundAnimation.Bite && isHit != 0)
            {
                hasPlayed = false;
                SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.chicken1, ref hasPlayed);
            }

            currentVelocity = 0f;
            freezeTime = FREEZE_TIME;
            if (disappearTime < FREEZE_TIME)
            {
                disappearTime = FREEZE_TIME;
            }

            attackTimes--;

            if (attackTimes == 0)
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
        if (currentVelocity > 0)
        {
            transform.GetChild(0).eulerAngles = AEntity.FRONT_DIRECTION;
        }
        else if (currentVelocity < 0)
        {
            transform.GetChild(0).eulerAngles = AEntity.BACK_DIRECTION;
        }
    }
}
