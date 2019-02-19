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
    public const float RESPAWN_TIMER = 3.0f;

    //Attributes
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentEnergy;
    private float moveHorizontal;

    protected ActorStat actorStat;

    private Vector3 frontDirection = new Vector3(0, 90, 0);
    private Vector3 backDirection = new Vector3(0, 270, 0);

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

    private float deathTimer = 0f;

    protected string deathAnimation = "";

    protected int respawnLives = 3;

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

    protected float DeathTimer
    {
        get
        {
            return deathTimer;
        }

        set
        {
            deathTimer = value;
        }
    }

    public AnimatorController GetAnimatorController()
    {
        return ac;
    }

    //Functions
    protected void InitializeActor()
    {
        state = new ActorStandingState();

        CurrentHealth = this.actorStat.MaxHealth;
        CurrentEnergy = this.actorStat.MaxEnergy;
    }

    public virtual float TakeDamage(float damage)
    {
        this.CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            state = new ActorDeathState();
            Death();
        }
        return CurrentHealth;
    }

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
        //Debug.Log(GetName() + " is jumping at " + actorStat.JumpVelocity * 111 + " velocity and " + Vector3.up * actorStat.JumpVelocity * 111 + " Force");
    }

    public abstract void Attack();

    public abstract void Block();

    public abstract void Grab();

    public virtual void Death()
    {
        GetAnimatorController().SetInt(GetDeathAnimation());
        deathTimer = AActor.RESPAWN_TIMER;
    }

    public virtual void Respawn()
    {
        InitializeActor();
        GameObject[] respawnObjects = GameObject.FindGameObjectsWithTag("Respawn");

        GameObject respawnLocation = respawnObjects[Random.Range(0, respawnObjects.Length)];

        if (respawnLocation)
        {
            this.transform.position = new Vector3(respawnLocation.transform.position.x, respawnLocation.transform.position.y, this.transform.position.z);
        }

        GetAnimatorController().SetInt("animation,13");
    }

    public ActorStat GetActorStat()
    {
        return this.actorStat;
    }

    public string GetDeathAnimation()
    {
        return deathAnimation;
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
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                //Back to standing after each attack
                //Debug.Log("Attack Timer for " + GetName() + " is " + AttackTimer);
                state = new ActorStandingState();
            }
        }

        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0 && respawnLives > 0) // && CanRespawn
            {
                respawnLives--;
                Respawn();
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
