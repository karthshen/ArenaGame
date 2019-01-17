using UnityEngine;
using System.Collections;

public class MageActor : AActor
{
    ActorState defaultState;
    Mesh mageMesh;

    Ability mageAbilityUp;
    Ability mageAbilityDown;
    Ability mageAbilityLeft;
    Ability mageAbilityRight;

    Command mageButtonA;
    Command mageButtonB;
    Command mageButtonC;
    Command mageButtonD;

    public MageActor()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        state = defaultState;
        entityMesh = mageMesh;

        //Actor Config, button, ability, etc
        actorStat = new MageStat();
        defaultState = new ActorStandingState();

        buttonA = mageButtonA;
        buttonB = mageButtonB;
        buttonC = mageButtonC;
        buttonD = mageButtonD;

        abilityLeft = mageAbilityLeft;
        abilityRight = mageAbilityRight;
        abilityUp = mageAbilityUp;
        abilityDown = mageAbilityDown;
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
