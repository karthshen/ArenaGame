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

    string actorName = "Mage";

    public MageActor() : base()
    {

    }

    private void Start()
    {
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
        mageMesh = new Mesh();
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
        base.Move();
    }

    public override void GenerateAttackQueue()
    {
        throw new System.NotImplementedException();
    }

    public override float TakeDamage(float damage)
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
}
