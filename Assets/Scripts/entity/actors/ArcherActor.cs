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

    string actorName = "Archer";

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
        actorStat = new ArcherStat();
        entityName = actorName;

        //Abilities set up

        abilityLeft = archerAbilityLeft;
        abilityRight = archerAbilityRight;
        abilityUp = archerAbilityUp;
        abilityDown = archerAbilityDown;

        //Default to Grounded
        IsGrounded = true;

        InitializeActor();

        NullParameterCheck();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Block()
    {
        BIsBlocking = true;
    }

    public override void Death()
    {
        base.Death();
    }

    public override void GenerateAirAttackQueue()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
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
