using UnityEngine;
using UnityEditor;

public class WarriorBoomrang : ProjectileItem
{
    enum BoomrangAnimation
    {
        SpinOut = 1,
        SpinIn = 2,
        SpinStay = 3
    }

    private float velocity = 15.0f;
    private float moveHorizontal = 1f;
    private float positionHorizontal = 0.05f;
    private bool hasPlayed = false;

    private Vector3 movement;

    private const float THROW_OUT_DURATION = 0.6f;
    private const float SPIN_STAY_DURATION = 0.3f;
    private float duration_time = 0f;

    private BoomrangAnimation boomrangState;

    private AudioSource audioSource;

    private Animator animator;

    private void Start()
    {

    }

    public override void ProjectileStart()
    {
        IgnoreOwnerCollision(owner);
        //SetRotationToEntity(owner);
        float yDirectionInRadian = owner.GetYDirectionInRadian();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        gameObject.transform.position = new Vector3(owner.transform.position.x + positionHorizontal * Mathf.Sin(yDirectionInRadian),
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);
        movement = new Vector3(moveHorizontal * Mathf.Sin(yDirectionInRadian) * velocity, 0.0f, 0.0f);

        boomrangState = BoomrangAnimation.SpinOut;
        PlayAnimation(boomrangState);
    }

    public override void ProjectileFinish()
    {
        RemoveItem();
    }

    private void Update()
    {
        if (boomrangState == BoomrangAnimation.SpinOut)
        {
            transform.Translate(movement * Time.deltaTime);
            duration_time += Time.deltaTime;
        }

        if (boomrangState == BoomrangAnimation.SpinIn)
        {
            float step = velocity * Time.deltaTime;

            Vector3 ownerPosition = new Vector3(owner.transform.position.x,
            owner.transform.position.y + owner.transform.lossyScale.y / 2, owner.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, ownerPosition, step / 2f);

            if (Vector3.Distance(transform.position, owner.transform.position) < 1f)
            {
                ProjectileFinish();
            }
            //PlayAnimation(boomrangState);
        }

        if (duration_time >= THROW_OUT_DURATION && boomrangState == BoomrangAnimation.SpinOut)
        {
            boomrangState = BoomrangAnimation.SpinStay;
            PlayAnimation(BoomrangAnimation.SpinIn);
            owner.AttackCode = System.Guid.NewGuid();
            duration_time = 0f;
        }

        if (boomrangState == BoomrangAnimation.SpinStay)
        {
            duration_time += Time.deltaTime;
            if (duration_time >= SPIN_STAY_DURATION)
            {
                boomrangState = BoomrangAnimation.SpinIn;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AActor hitActor = collision.gameObject.GetComponent<AActor>();

        if (hitActor)
        {
            hitActor.TakeDamage(owner.GetActorStat().AttackPower, owner);
        }
        else if (collision.gameObject.GetComponent<PickupItem>())
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponentInChildren<Collider>());
            return;
        }
        else if (boomrangState == BoomrangAnimation.SpinIn)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponentInChildren<Collider>());
            return;
        }
        else if(!collision.gameObject.GetComponent<AEntity>())
        {
            boomrangState = BoomrangAnimation.SpinStay;
        }
    }

    private void PlayAnimation(BoomrangAnimation anim)
    {
        animator.SetInteger("animation", (int)anim);
        SoundManager.instance.PlayEffectWithAudioSource(audioSource, SoundManager.instance.boomrang, ref hasPlayed);
    }
}