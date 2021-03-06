﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarriorActor : AActor
{
    Mesh warriorMesh;

    Ability warriorAbilityUp;
    Ability warriorAbilityNeutral;
    Ability warriorAbilityDown;
    Ability warriorAbilityLeft;
    Ability warriorAbilityRight;
    Ability warriorAbilityTrigger;

    string actorName = "Warrior";

    public WarriorSword sword;

    public WarriorShield shield;

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
        effectSource = GetComponent<AudioSource>();

        //Actor Config, button, ability, etc
        actorStat = new WarriorData();
        entityName = actorName;

        warriorAbilityLeft = new WarriorThrowBoomrang(this);
        warriorAbilityRight = new WarriorThrowBoomrang(this);
        warriorAbilityDown = new WarriorSlash(this);
        warriorAbilityUp = new WarriorUpwardSlash(this);
        warriorAbilityTrigger = new WarriorShootMechanicalHook(this);
        warriorAbilityNeutral = new WarriorCharge(this);

        abilityLeft = warriorAbilityLeft;
        abilityRight = warriorAbilityRight;
        abilityUp = warriorAbilityUp;
        abilityDown = warriorAbilityDown;
        abilityTrigger = warriorAbilityTrigger;
        abilityNeutral = warriorAbilityNeutral;

        InitializeActor();

        if (sword)
        {
            sword.ItemPickUp(this);
        }

        if (shield)
        {
            shield.ItemPickUp(this);
        }

        NullParameterCheck();
    }

    public override void Attack()
    {
        //Write code to capture the target enemy
        sword.UseItem(this);
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
        attackQueue.Enqueue(Combo.Null);
    }

    public override void GenerateAirAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Attack1);
        attackQueue.Enqueue(Combo.Attack2);
    }

    public override float TakeDamage(float damage, AActor attacker)
    {
        if (BIsBlocking)
        {
            damage /= 5;
        }
        return base.TakeDamage(damage, attacker);
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
