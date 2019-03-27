﻿using UnityEngine;
using System.Collections.Generic;
using InControl;
using System;

public abstract class AActor : AEntity
{
    //Enums
    public enum Combo
    {
        Null,
        Attack0,
        Attack1,
        Attack2
    }

    //constants
    public const float ATTACK_TIMER_BETWEEN_COMBO = 0.7f;
    public const float ATTACK_TIMER = ATTACK_TIMER_BETWEEN_COMBO / 1.3f;
    public const float ATTACK_INTERVAL = 0.35f / 1.3f;
    public const float CAST_DURATION = 0.5f;
    public const float AIR_ATTACK_LENGTH = ATTACK_INTERVAL;
    public const float RESPAWN_TIMER = 3.0f;
    public const float AIRBORNE_DRAG = 15.0f;
    public const float DAMAGE_TO_ENERGY_CONSTANT = 20f;

    private const float FREEZEING_TIME_DEFAULT = 1.0f / 1000f * 85f;

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

    //Fake State
    private bool bIsGrounded = false;

    private bool bIsBlocking = false;
    //---------------------------

    //Timer
    protected float attackTimer = 0f;

    private float deathTimer = 0f;

    private float castTimer = 0f;

    private float energyRegTimer = 0f;

    private float freezeTimer = 0f;
    //----------------------------

    protected string deathAnimation = "";

    private int respawnLives = 3;

    //This is for total damage taken since previous energy restore
    private float totalDamageTaken = 0;

    //Attack Code and Damage Code - In order to avoid multiple damage with single attack
    private Guid attackCode = Guid.NewGuid();
    private Guid damageCode = Guid.NewGuid();

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
            if(currentHealth > GetActorStat().MaxHealth)
            {
                currentHealth = GetActorStat().MaxHealth;
            }
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
            if(currentEnergy > GetActorStat().MaxEnergy)
            {
                currentEnergy = GetActorStat().MaxEnergy;
            }
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

    public float FreezeTimer
    {
        get
        {
            return freezeTimer;
        }

        set
        {
            freezeTimer = value;
        }
    }

    public Guid AttackCode
    {
        get
        {
            return attackCode;
        }

        set
        {
            attackCode = value;
        }
    }

    public Guid DamageCode
    {
        get
        {
            return damageCode;
        }

        set
        {
            damageCode = value;
        }
    }

    public int RespawnLives
    {
        get
        {
            return respawnLives;
        }

        set
        {
            respawnLives = value;
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

    public virtual void KnockBack(float knockingForce, AActor attacker)
    {
        //Knocking back
        float yDirectionInRadian = attacker.transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        Vector3 backMovement = new Vector3(knockingForce * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        GetRigidbody().AddForce(backMovement);

        state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
    }

    public virtual float TakeDamage(float damage, AActor attacker)
    {
        if (state.GetType() == typeof(ActorDeathState))
            return 0;

        if (attacker.AttackCode.Equals(damageCode))
        {
            return 0;
        }

        this.CurrentHealth -= damage;
        damageCode = attacker.AttackCode;

        //The attacked actor goes to freeze state
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Death();
        }
        else if(state.GetType() == typeof(ActorBlockState))
        {

        }
        else
        {
            state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
        }


        //Damage to Energy
        totalDamageTaken += damage;
        if(totalDamageTaken >= DAMAGE_TO_ENERGY_CONSTANT)
        {
            if(this.currentEnergy <= ActorStat.MaxEnergy)
            {
                currentEnergy++;
                totalDamageTaken -= DAMAGE_TO_ENERGY_CONSTANT;
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

        GameObject respawnLocation = respawnObjects[UnityEngine.Random.Range(0, respawnObjects.Length)];

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

    /*
     * The first element is ignored
     */
    public abstract void GenerateAttackQueue();

    /*
     * The first element is ignored
     */
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
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0 && respawnLives > 0) // && CanRespawn
            {
                respawnLives--;
                Respawn();
            }

            return;
        }

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
                BackToStanding();
                attackTimer = ATTACK_TIMER_BETWEEN_COMBO - ATTACK_INTERVAL;
            }
        }

        if (CastTimer > 0)
        {
            CastTimer -= Time.deltaTime;
            if (CastTimer <= 0)
            {
                BackToStanding();
            }
        }

        if (transform.position.y < -20.0f)
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

        if(freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            if(freezeTimer <= 0)
            {
                freezeTimer = 0;
                BackToStanding();
            }
        }
    }

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == false && state.GetType() != typeof(ActorDeathState))
        {
            bIsGrounded = true;
            BackToStanding();
            //Debug.Log("Entering StandingState from Ground");
        }

        if (collision.gameObject.GetComponent<PickupItem>())
        {
            PickupItem item = collision.gameObject.GetComponent<PickupItem>();
            item.ItemPickUp(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == true)
        {
            bIsGrounded = false;
        }
    }

    private void BackToStanding()
    {
        if(state.GetType() != typeof(ActorDeathState))
        {
            state = new ActorStandingState(state.GetType().ToString());
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
