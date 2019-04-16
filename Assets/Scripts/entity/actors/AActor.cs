using UnityEngine;
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
    public float ATTACK_INTERVAL = 0.35f / 1.3f;
    public float AIR_ATTACK_LENGTH = 0.35f / 1.3f;
    public const float CAST_DURATION = 0.5f;
    public const float RESPAWN_TIMER = 3.0f;
    public const float AIRBORNE_DRAG = 15.0f;
    public const float DAMAGE_TO_ENERGY_CONSTANT = 20f;

    public int playerNum;

    private const float FREEZEING_TIME_DEFAULT = 1.0f / 1000f * 85f;

    //Victory Sign
    public GameObject winSymbol;

    //Attributes
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentEnergy;
    
    private float jumpForceFactor = 91f;
    private float moveHorizontal;
    private float moveVertical;

    protected ActorData actorStat;

    private Vector3 frontDirection = new Vector3(0, 90, 0);
    private Vector3 backDirection = new Vector3(0, 270, 0);

    public Ability abilityUp;
    public Ability abilityDown;
    public Ability abilityLeft;
    public Ability abilityRight;
    public Ability abilityTrigger;
    public Ability abilityBumper;
    public Ability abilityNeutral;

    public Queue<Combo> attackQueue = new Queue<Combo>();

    protected AnimatorController ac;

    protected PickupItem item = null;

    protected Rigidbody rb;

    //Fake State
    private bool bIsGrounded = false;

    private bool bIsBlocking = false;

    private int jumpNum = 0;

    private bool abilityCastedInAir = false;
    //---------------------------

    //Timer
    protected float attackTimer = 0f;

    private float deathTimer = 0f;

    private float castTimer = 0f;

    private float energyRegTimer = 0f;

    private float freezeTimer = 0f;
    //----------------------------
    private int respawnLives = 3;

    private float totalDamageDealt = 0f;

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
            if (currentHealth > GetActorStat().MaxHealth)
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
            if (currentEnergy > GetActorStat().MaxEnergy)
            {
                currentEnergy = GetActorStat().MaxEnergy;
            }
        }
    }

    protected ActorData ActorStat
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

    public bool AbilityCastedInAir
    {
        get
        {
            return abilityCastedInAir;
        }

        set
        {
            abilityCastedInAir = value;
        }
    }

    public int JumpNum
    {
        get
        {
            return jumpNum;
        }

        set
        {
            jumpNum = value;
        }
    }

    public float MoveVertical
    {
        get
        {
            return moveVertical;
        }

        set
        {
            moveVertical = value;
        }
    }

    public float TotalDamageDealt
    {
        get
        {
            return totalDamageDealt;
        }

        set
        {
            totalDamageDealt = value;
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

        actorStat.MaxHealth = GameStageSetting.PlayerStartingHealth;

        CurrentHealth = actorStat.MaxHealth;
        CurrentEnergy = actorStat.MaxEnergy;
    }

    public virtual void KnockBack(float knockingForce, AActor attacker)
    {
        KnockBackBasedOnPosition(knockingForce, attacker);
    }

    private void KnockBackBasedOnDirection(float knockingForce, AActor attacker)
    {
        //Knocking back
        float yDirectionInRadian = -attacker.GetYDirectionInRadian();

        Vector3 backMovement = new Vector3(knockingForce * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        GetRigidbody().AddForce(backMovement);

        if(state.GetType() != typeof(ActorDeathState))
            state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
    }

    private void KnockBackBasedOnPosition(float knockingForce, AActor attacker)
    {
        float leftDirectionInRadian = 270f * Mathf.PI / 180;
        float rightDirectionInRadian = 90f * Mathf.PI / 180;

        Vector3 backMovement;

        if (transform.position.x < attacker.transform.position.x)
        {
            backMovement = new Vector3(knockingForce * Mathf.Sin(leftDirectionInRadian), 0f, 0f);
        }
        else if (transform.position.x > attacker.transform.position.x)
        {
            backMovement = new Vector3(knockingForce * Mathf.Sin(rightDirectionInRadian), 0f, 0f);
        }
        else
        {
            backMovement = new Vector3(0f, 0f, 0f);
        }

        GetRigidbody().AddForce(backMovement);

        if (state.GetType() != typeof(ActorDeathState))
            state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
    }

    public virtual float TakeDamageAndFreeze(float damage, float freezeTime, AActor attacker)
    {
        TakeDamage(damage, attacker);

        if (state.GetType() != typeof(ActorDeathState))
        {
            freezeTimer = 0;
            state = new ActorFreezeState(freezeTime, this, attacker, 0);
        }
        return CurrentHealth;
    }

    public virtual float FreezeWithNoForce(float freezeTime, AActor attacker)
    {
        freezeTime = 0f;
        state = new ActorFreezeState(freezeTime, this, attacker, 0);
        return CurrentHealth;
    }

    public float TakeDamageFromEntity(float damage, float knockingForce, AEntity attacker)
    {
        AttackCode = System.Guid.NewGuid();
        float isHit =  TakeDamage(damage, this);
        KnockBackBasedOnDirection(knockingForce, this);

        return isHit;
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
        if (CurrentHealth <= 0.001)
        {
            CurrentHealth = 0;
            Death();
        }
        else if (state.GetType() == typeof(ActorBlockState))
        {

        }
        else
        {
            freezeTimer = 0;
            if (state.GetType() != typeof(ActorDeathState))
                state = new ActorFreezeState(FREEZEING_TIME_DEFAULT, this, attacker);
        }


        //Damage to Energy
        totalDamageTaken += damage;
        if (totalDamageTaken >= DAMAGE_TO_ENERGY_CONSTANT)
        {
            if (this.currentEnergy <= ActorStat.MaxEnergy)
            {
                currentEnergy++;
                totalDamageTaken -= DAMAGE_TO_ENERGY_CONSTANT;
            }
            else
            {
                totalDamageTaken = 0;
            }
        }

        //Add to total damage on attaccker
        attacker.TotalDamageDealt += damage;

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
        ClearForceOnActor();

        Vector3 forceJump = Vector3.up * actorStat.JumpVelocity * jumpForceFactor;

        if(jumpNum == 0)
        {
            forceJump *= 1.1f;
        }

        rb.AddForce(forceJump);
    }

    public abstract void Attack();

    public abstract void Block();

    public abstract void Unblock();

    public abstract void Grab();

    public virtual void Death()
    {
        state = new ActorDeathState();
        ((ActorState)state).PlayStateAnimation(this);
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

        GetAnimatorController().SetInt(GetActorStat().IdleAnimation);
    }

    public ActorData GetActorStat()
    {
        return this.actorStat;
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

    public void ClearForceOnActor()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    protected void ActorUpdate()
    {
        //If Lock Energy Enabled, lock the energy bar
        if (GameStageSetting.LockEnergy && currentEnergy < actorStat.MaxEnergy)
        {
            currentEnergy = actorStat.MaxEnergy;
        }

        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0) // && CanRespawn
            {
                respawnLives--;
                if (respawnLives > 0)
                {
                    Respawn();
                }
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
                GetRigidbody().drag = 0;
                BackToStanding();
            }
        }

        FallOffPlatformCheck();

        if (currentEnergy < ActorStat.MaxEnergy)
        {
            energyRegTimer += Time.deltaTime;
            if (energyRegTimer >= ActorStat.EnergyRegenerationTime)
            {
                currentEnergy++;
                energyRegTimer -= ActorStat.EnergyRegenerationTime;
            }
        }

        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                freezeTimer = 0;
                BackToStanding();
            }
        }
    }

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == false && state.GetType() != typeof(ActorDeathState) && state.GetType() != typeof(ActorFreezeState))
        {
            bIsGrounded = true;
            BackToStanding();
            //Debug.Log("Entering StandingState from Ground");
        }

        if (collision.gameObject.GetComponent<PickupItem>() && collision.gameObject.GetComponent<PickupItem>().GetOwner() != this)
        {
            PickupItem item = collision.gameObject.GetComponent<PickupItem>();
            item.ItemPickUp(this);
        }

        if(!IsGrounded && state.GetType() == typeof(ActorJumpState) && (collision.gameObject.GetComponent<AActor>() && transform.position.y > collision.transform.position.y))
        {
            BackToStanding();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Ground" && bIsGrounded == false && (state.GetType() != typeof(ActorDeathState)) && state.GetType()!= typeof(ActorFreezeState)))
        {
            bIsGrounded = true;
            BackToStanding();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && bIsGrounded == true && jumpNum == 0)
        {
            bIsGrounded = false;
            jumpNum = 2;
        }
    }

    protected virtual void BackToStanding()
    {
        if (state.GetType() != typeof(ActorDeathState))
        {
            state = new ActorStandingState(state.GetType().ToString());
            ((ActorState)(state)).PlayStateAnimation(this);
        }
    }

    public void Victory(Scoreboard scoreboard)
    {
        scoreboard.gameObject.SetActive(true);
        scoreboard.SetWinner(GetName());
        scoreboard.winnerPlayerNum.text = playerNum.ToString();
        scoreboard.winnerTotalDamage.text = ((int)TotalDamageDealt).ToString();
    }

    public void Lose(Scoreboard scoreboard)
    {
        scoreboard.SetLoser(GetName());
        scoreboard.loserPlayerNum.text = playerNum.ToString();
        scoreboard.loserTotalDamage.text = ((int)TotalDamageDealt).ToString();
    }

    private void FallOffPlatformCheck()
    {
        if (transform.position.y < -12.0f)
        {
            if (GetRigidbody())
                GetRigidbody().isKinematic = true;

            Death();
            deathTimer = 0.3f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Collider[] collidersToIgnore = GetComponentsInChildren<Collider>();

            foreach (Collider collider in collidersToIgnore)
            {
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), collider, true);
                Physics.IgnoreCollision(other.gameObject.GetComponentInChildren<Collider>(), collider, true);
            }

            Collider objectCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), objectCollider, true);
            Physics.IgnoreCollision(other.gameObject.GetComponentInChildren<Collider>(), objectCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            Collider[] collidersToIgnore = GetComponentsInChildren<Collider>();

            foreach (Collider collider in collidersToIgnore)
            {
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), collider, false);
                Physics.IgnoreCollision(other.gameObject.GetComponentInChildren<Collider>(), collider, false);
            }

            Collider objectCollider = GetComponent<Collider>();
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), objectCollider, false);
            Physics.IgnoreCollision(other.gameObject.GetComponentInChildren<Collider>(), objectCollider, false);
        }
    }
}
