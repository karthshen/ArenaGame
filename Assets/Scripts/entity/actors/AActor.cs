using UnityEngine;
using System.Collections.Generic;
using InControl;

public abstract class AActor : AEntity
{
    //Enums
    public enum Combo
    {
        Attack0,
        Attack1,
        Attack2
    }

    //constants
    public const float ATTACK_TIMER = 0.7f;
    public const float ATTACK_INTERVAL = 0.35f;

    //Attributes
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentEnergy;
    private float moveHorizontal;

    private Vector3 frontDirection = new Vector3(0, 90, 0);
    private Vector3 backDirection = new Vector3(0, 270, 0);

    protected ActorStat actorStat;

    protected Ability abilityUp;
    protected Ability abilityDown;
    protected Ability abilityLeft;
    protected Ability abilityRight;

    public Queue<Combo> attackQueue = new Queue<Combo>();

    protected AnimatorController ac;

    protected PickupItem item = null;

    protected Rigidbody rb;

    private bool bIsGrounded = false;

    protected float attackTimer = 0f;

    //Mutators
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    protected float CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }

        set
        {
            currentEnergy = value;
        }
    }

    protected ActorStat ActorStat
    {
        get
        {
            return actorStat;
        }

        set
        {
            actorStat = value;
        }
    }

    public float MoveHorizontal
    {
        get
        {
            return moveHorizontal;
        }

        set
        {
            moveHorizontal = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return bIsGrounded;
        }

        set
        {
            bIsGrounded = value;
        }
    }

    public float AttackTimer
    {
        get
        {
            return attackTimer;
        }

        set
        {
            attackTimer = value;
        }
    }

    public AnimatorController GetAnimatorController()
    {
        return ac;
    }

    //Functiosn
    public abstract float TakeDamage(float damage);

    public void HandleInput(InputDevice inputDevice)
    {
        ActorState newState = ((ActorState)state).HandleInput(this, inputDevice);
        if(state != null)
        {
            //Debug.Log("CurrentState:" + state.GetType());
            state = newState;
        }
    }

    public virtual void Move()
    {
        //TODO Improve the movement code
        TurnAround();
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Translate(movement * actorStat.MoveVelocity * Time.deltaTime);
        //rb.AddForce(movement * actorStat.MoveVelocity);
    }

    public virtual void Jump()
    {
        //rb.AddForce(Vector3.up * actorStat.JumpVelocity);
        GetRigidbody().velocity = Vector3.zero;
        GetRigidbody().angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * actorStat.JumpVelocity * 111);
    }

    public abstract void Attack();

    public abstract void Block();

    public abstract void Grab();

    public ActorStat GetActorStat()
    {
        return this.actorStat;
    }

    public abstract void GenerateAttackQueue();

    public new void NullParameterCheck()
    {
        base.NullParameterCheck();
        if (actorStat == null)
        {
            throw new MissingReferenceException("Actor stat/state is not set for " + this.entityName + ": " + this.GetEntityId());
        }

        //if (abilityUp.Equals(null) || abilityDown.Equals(null) || abilityLeft.Equals(null) || abilityRight.Equals(null))
        //{
        //    throw new MissingReferenceException("Actor ability configuration missing for " + this.name + ": " + this.GetEntityId());
        //}
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    protected void ActorUpdate()
    {
        if (attackTimer >= 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                //Back to standing after each attack
                //Debug.Log("Attack Timer for " + GetName() + " is " + AttackTimer);
                state = new ActorStandingState();
            }
        }
    }

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && this.state.GetType() == typeof(ActorJumpState))
        {
            bIsGrounded = true;
            this.state = new ActorStandingState();
            //Debug.Log("Entering StandingState from Ground");
        }
    }

    private void TurnAround()
    {
        if(moveHorizontal > 0)
        {
            transform.GetChild(0).eulerAngles = frontDirection;
        }
        else if (moveHorizontal < 0)
        {
            transform.GetChild(0).eulerAngles = backDirection;
        }
    }
}
