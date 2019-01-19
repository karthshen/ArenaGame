using UnityEngine;
using System.Collections;

public class MageActor : AActor
{
    ActorState defaultState;
    Mesh mageMesh = new Mesh();

    Ability mageAbilityUp;
    Ability mageAbilityDown;
    Ability mageAbilityLeft;
    Ability mageAbilityRight;

    string actorName = "Mage";

    public MageActor()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
        entityMesh = mageMesh;

        //Actor Config, button, ability, etc
        actorStat = new MageStat();
        entityName = actorName;

        abilityLeft = mageAbilityLeft;
        abilityRight = mageAbilityRight;
        abilityUp = mageAbilityUp;
        abilityDown = mageAbilityDown;

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
