﻿using UnityEngine;
using System.Collections;

public abstract class AActor : AEntity
{
    //Attributes
    private float currentHealth;
    private float currentEnergy;

    protected ActorStat actorStat;

    protected Ability abilityUp;
    protected Ability abilityDown;
    protected Ability abilityLeft;
    protected Ability abilityRight;

    protected PickupItem item = null;

    protected AActor() : base()
    { 

    }

    //Mutators
    protected float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    protected float CurrentEnergy
    {
        get
        {
            return currentEnergy;
        }

        set
        {
            currentEnergy = value;
        }
    }

    protected ActorStat ActorStat
    {
        get
        {
            return actorStat;
        }

        set
        {
            actorStat = value;
        }
    }

    //Functiosn
    public abstract float TakeDamage(float damage);

    public void HandleInput()
    {
        ActorState newState = ((ActorState)state).HandleInput(this);
        if (!state.Equals(null))
        {
            state = newState;
        }
    }

    public abstract void Move();

    public abstract void Jump();

    public abstract void Attack();

    public abstract void Block();

    public abstract void Grab();

    public ActorStat GetActorStat()
    {
        return this.actorStat;
    }

    public new void NullParameterCheck()
    {
        base.NullParameterCheck();
        if (actorStat == null)
        {
            throw new MissingReferenceException("Actor stat/state is not set for " + this.entityName + ": " + this.GetEntityId());
        }

        //if (abilityUp.Equals(null) || abilityDown.Equals(null) || abilityLeft.Equals(null) || abilityRight.Equals(null))
        //{
        //    throw new MissingReferenceException("Actor ability configuration missing for " + this.name + ": " + this.GetEntityId());
        //}
    }
}
