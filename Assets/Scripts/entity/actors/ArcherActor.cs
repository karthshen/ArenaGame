using UnityEngine;
using System.Collections;

public class ArcherActor : AActor
{
    ActorState defaultState;
    Mesh archerMesh = new Mesh();

    Ability archerAbilityUp;
    Ability archerAbilityDown;
    Ability archerAbilityLeft;
    Ability archerAbilityRight;

    string actorName = "Archer";

    public ArcherActor()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
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
        throw new System.NotImplementedException();
    }

    public override float TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
