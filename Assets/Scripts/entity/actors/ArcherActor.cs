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
        archerMesh = new Mesh();
        entityMesh = archerMesh;

        //Actor Config, button, ability, etc
        actorStat = new ArcherStat();
        entityName = actorName;

        abilityLeft = archerAbilityLeft;
        abilityRight = archerAbilityRight;
        abilityUp = archerAbilityUp;
        abilityDown = archerAbilityDown;

        NullParameterCheck();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Block()
    {
        throw new System.NotImplementedException();
    }

    public override void Grab()
    {
        throw new System.NotImplementedException();
    }

    public override void Jump()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    /*
     * This is for test cases ONLY
     */
    public void CallStart()
    {
        Start();
    }

    public override void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void GenerateAirAttackQueue()
    {
        throw new System.NotImplementedException();
    }

    public override void Unblock()
    {
        throw new System.NotImplementedException();
    }
}
