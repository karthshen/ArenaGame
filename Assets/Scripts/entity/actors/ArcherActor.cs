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

    public ArcherActor()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        state = defaultState;
        entityMesh = archerMesh;

        //Actor Config, button, ability, etc
        actorStat = new ArcherStat();
        defaultState = new ActorStandingState();

        abilityLeft = archerAbilityLeft;
        abilityRight = archerAbilityRight;
        abilityUp = archerAbilityUp;
        abilityDown = archerAbilityDown;
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

    public override void HandleInput()
    {
        ActorState newState = defaultState.HandleInput(this);
        if (!defaultState.Equals(null))
        {
            defaultState = newState;
        }
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
