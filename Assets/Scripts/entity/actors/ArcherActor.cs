using UnityEngine;
using System.Collections;

public class ArcherActor : AActor
{
    ActorState defaultState;
    Mesh archerMesh;

    Ability archerAbilityUp;
    Ability archerAbilityDown;
    Ability archerAbilityLeft;
    Ability archerAbilityRight;
    Ability archerAbilityTrigger;

    string actorName = "Archer";

    public ArcherBow bow;

    public ArcherActor() : base()
    {

    }

    private void Start()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
        entityMesh = archerMesh;

        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimatorController>();
        //Actor Config, button, ability, etc
        actorStat = new ArcherData();
        entityName = actorName;

        //Abilities set up
        archerAbilityDown = new ArcherBurstShot(this);
        archerAbilityRight = new ArcherDeployTrap(this);
        archerAbilityLeft = new ArcherDeployTrap(this);
        archerAbilityUp = new ArcherUnleshTheHound(this);
        archerAbilityTrigger = new ArcherShootClawhook(this);

        abilityLeft = archerAbilityLeft;
        abilityRight = archerAbilityRight;
        abilityUp = archerAbilityUp;
        abilityDown = archerAbilityDown;
        abilityTrigger = archerAbilityTrigger;

        //Default to Grounded
        IsGrounded = true;

        if (bow)
        {
            bow.ItemPickUp(this);
        }

        InitializeActor();

        NullParameterCheck();
    }

    public override void Attack()
    {
        bow.UseItem(this);
    }

    public override void Block()
    {
        BIsBlocking = true;
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Unblock()
    {
        BIsBlocking = false;
    }

    public override void Grab()
    {
        throw new System.NotImplementedException();
    }

    public override void Jump()
    {
        base.Jump();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void GenerateAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Null);
    }

    public override void GenerateAirAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        //attackQueue.Enqueue(Combo.Attack1);
    }

    public override float TakeDamage(float damage, AActor attacker)
    {
        return base.TakeDamage(damage, attacker);
    }

    //MonoBehavior Functions
    private void Update()
    {
        base.ActorUpdate();
    }


    /*
     * This is for test cases ONLY
     */
    public void CallStart()
    {
        Start();
    }
}
