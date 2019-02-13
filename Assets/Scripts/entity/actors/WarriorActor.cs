using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        ac = GetComponent<AnimatorController>();

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
        Debug.Log("Warrior Attacked");
        //Write code to capture the target enemy


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
        base.Jump();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void GenerateAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Attack1);
        attackQueue.Enqueue(Combo.Attack2);
    }

    public override float TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
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
