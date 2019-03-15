﻿using UnityEngine;
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
    public const float ATTACK_TIMER = 0.7f / 1.3f;
    public const float ATTACK_INTERVAL = 0.35f / 1.3f;
    public const float CAST_DURATION = 0.5f;
    public const float AIR_ATTACK_LENGTH = ATTACK_INTERVAL;
    public const float RESPAWN_TIMER = 3.0f;
    public const float AIRBORNE_DRAG = 15.0f;
    public const float DAMAGE_TO_HEALTH_CONSTANT = 20f;

    //Attributes
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentEnergy;
    private float moveHorizontal;

    protected ActorStat actorStat;

    private Vector3 frontDirection = new Vector3(0, 90, 0);
    private Vector3 backDirection = new Vector3(0, 270, 0);

    public Ability abilityUp;
    public Ability abilityDown;
    public Ability abilityLeft;
    public Ability abilityRight;

    public Queue<Combo> attackQueue = new Queue<Combo>();

    protected AnimatorController ac;

    protected PickupItem item = null;

    protected Rigidbody rb;

    private bool bIsGrounded = false;

    private bool bIsBlocking = false;

    protected float attackTimer = 0f;

    private float deathTimer = 0f;

    private float castTimer = 0f;

    private float energyRegTimer = 0f;

    protected string deathAnimation = "";

    protected int respawnLives = 3;

    //This is for total damage taken since previous energy restore
    private float totalDamageTaken = 0;

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

    public float CurrentEnergy
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

    public bool BIsBlocking
    {
        get
        {
            return bIsBlocking;
        }

        set
        {
            bIsBlocking = value;
        }
    }

    public float CastTimer
    {
        get
        {
            return castTimer;
        }

        set
        {
            castTimer = value;
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

        if (GetRigidbody())
            GetRigidbody().isKinematic = false;

        CurrentHealth = actorStat.MaxHealth;
        CurrentEnergy = actorStat.MaxEnergy;
    }

    public virtual float TakeDamage(float damage)
    {
        this.CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Death();
        }

        totalDamageTaken += damage;
        if(totalDamageTaken >= DAMAGE_TO_HEALTH_CONSTANT)
        {
            if(this.currentEnergy <= ActorStat.MaxEnergy)
            {
                currentEnergy++;
                totalDamageTaken -= DAMAGE_TO_HEALTH_CONSTANT;
            }
            else
            {
                totalDamageTaken = 0;
            }
        }

        return CurrentHealth;
    }

    public void HandleInput(InputDevice inputDevice)
    {
        ActorState newState = ((ActorState)state).HandleInput(this, inputDevice);
        if (state != null)
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

    public abstract void Unblock();

    public abstract void Grab();

    public virtual void Death()
    {
        state = new ActorDeathState();
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

    public abstract void GenerateAirAttackQueue();

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

            if (attackTimer < AIR_ATTACK_LENGTH)
            {
                GetRigidbody().drag = 0;
            }

            if (attackTimer <= 0 && (state.GetType() == typeof(ActorAttackState) || state.GetType() == typeof(ActorAirAttackState)))
            {
                //Back to standing after each attack
                //Debug.Log("Attack Timer for " + GetName() + " is " + AttackTimer);
                state = new ActorStandingState();
            }
        }

        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0 && respawnLives > 0) // && CanRespawn
            {
                respawnLives--;
                Respawn();
            }
        }

        if (CastTimer > 0)
        {
            CastTimer -= Time.deltaTime;
            if (CastTimer <= 0)
            {
                state = new ActorStandingState();
            }
        }

        if (transform.position.y < -20.0f && this.state.GetType() != typeof(ActorDeathState))
        {
            if (GetRigidbody())
                GetRigidbody().isKinematic = true;

            Death();
            deathTimer = 0.1f;
        }

        if(currentEnergy < ActorStat.MaxEnergy)
        {
            energyRegTimer += Time.deltaTime;
            if(energyRegTimer >= ActorStat.EnergyRegenerationTime)
            {
                currentEnergy++;
                energyRegTimer -= ActorStat.EnergyRegenerationTime;
            }
        }
    }

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == false)
        {
            bIsGrounded = true;
            this.state = new ActorStandingState();
            //Debug.Log("Entering StandingState from Ground");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == true)
        {
            bIsGrounded = false;
        }
    }

    private void TurnAround()
    {
        if (moveHorizontal > 0)
        {
            transform.GetChild(0).eulerAngles = frontDirection;
        }
        else if (moveHorizontal < 0)
        {
            transform.GetChild(0).eulerAngles = backDirection;
        }
    }
}
