using UnityEngine;
using System.Collections;

public class WarriorActor : AActor
{
    ActorState defaultState;
    Mesh warriorMesh;

    Ability warriorAbilityUp;
    Ability warriorAbilityDown;
    Ability warriorAbilityLeft;
    Ability warriorAbilityRight;

    Command warriorButtonA;
    Command warriorButtonB;
    Command warriorButtonC;
    Command warriorButtonD;

    public WarriorActor() : base()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        state = defaultState;
        entityMesh = warriorMesh;

        //Actor Config, button, ability, etc
        actorStat = new WarriorStat();
        defaultState = new ActorStandingState();

        buttonA = warriorButtonA;
        buttonB = warriorButtonB;
        buttonC = warriorButtonC;
        buttonD = warriorButtonD;

        abilityLeft = warriorAbilityLeft;
        abilityRight = warriorAbilityRight;
        abilityUp = warriorAbilityUp;
        abilityDown = warriorAbilityDown;
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
