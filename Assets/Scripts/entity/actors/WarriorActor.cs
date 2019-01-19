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

    string actorName = "Warrior";

    public WarriorActor() : base()
    {
        
    }

    void Start()
    {
        
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
        entityMesh = warriorMesh;
        rb = GetComponent<Rigidbody>();

        //Actor Config, button, ability, etc
        actorStat = new WarriorStat();
        entityName = actorName;

        abilityLeft = warriorAbilityLeft;
        abilityRight = warriorAbilityRight;
        abilityUp = warriorAbilityUp;
        abilityDown = warriorAbilityDown;

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

    public new void Move()
    {
        base.Move();
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
