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

    Command archerButtonA;
    Command archerButtonB;
    Command archerButtonC;
    Command archerButtonD;

    public ArcherActor()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        state = defaultState;
        entityMesh = archerMesh;

        //Actor Config, button, ability, etc
        actorStat = new ArcherStat();

        buttonA = archerButtonA;
        buttonB = archerButtonB;
        buttonC = archerButtonC;
        buttonD = archerButtonD;

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

    public override Command HandleInput()
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
