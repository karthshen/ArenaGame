using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarriorActor : AActor
{
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
        warriorMesh = new Mesh();
        entityMesh = warriorMesh;
        entityId = System.Guid.NewGuid();
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimatorController>();

        //Actor Config, button, ability, etc
        actorStat = new WarriorStat();
        entityName = actorName;

        warriorAbilityDown = new WarriorCharge(this);

        abilityLeft = warriorAbilityLeft;
        abilityRight = warriorAbilityRight;
        abilityUp = warriorAbilityUp;
        abilityDown = warriorAbilityDown;

        deathAnimation = "animation,10";

        InitializeActor();

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
        this.BIsBlocking = true;
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

    public override void GenerateAirAttackQueue()
    {
        attackQueue.Clear();
        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Attack1);
        attackQueue.Enqueue(Combo.Attack2);
    }

    public override float TakeDamage(float damage)
    {
        if (BIsBlocking)
        {
            damage /= 10;
        }
        return base.TakeDamage(damage);
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

    public override void Death()
    {
        base.Death();
    }

    public override void Unblock()
    {
        BIsBlocking = false;
    }
}
