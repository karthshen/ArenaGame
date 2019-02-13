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

    public WarriorSword sword;

    public WarriorActor() : base()
    {
        
    }

    void Start()
    {
        
        //Entity Config
        entityId = System.Guid.NewGuid();
        defaultState = new ActorStandingState();
        state = defaultState;
        warriorMesh = new Mesh();
        entityMesh = warriorMesh;
        rb = GetComponent<Rigidbody>();

        ac = GetComponent<AnimatorController>();

        //Actor Config, button, ability, etc
        actorStat = new WarriorStat();

        CurrentHealth = this.actorStat.MaxHealth;
        CurrentEnergy = this.actorStat.MaxEnergy;
        entityName = actorName;

        abilityLeft = warriorAbilityLeft;
        abilityRight = warriorAbilityRight;
        abilityUp = warriorAbilityUp;
        abilityDown = warriorAbilityDown;

        if (sword)
        {
            sword.ItemPickUp(this);
        }

        NullParameterCheck();
    }

    public override void Attack()
    {
        //Write code to capture the target enemy
        sword.UseItem();
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
        this.CurrentHealth -= damage;
        if(CurrentHealth < 0)
        {
            Debug.Log("Warrior Lost");
        }
        return CurrentHealth;
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
