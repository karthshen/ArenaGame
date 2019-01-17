using UnityEngine;
using System.Collections;

public abstract class AActor : AEntity
{
    //Attributes
    private float currentHealth;
    private float currentEnergy;

    protected ActorStat actorStat;

    protected Command buttonA;
    protected Command buttonB;
    protected Command buttonC;
    protected Command buttonD;

    protected Ability abilityUp;
    protected Ability abilityDown;
    protected Ability abilityLeft;
    protected Ability abilityRight;

    protected PickupItem item = null;

    protected AActor() : base()
    { 
        if (actorStat.Equals(null))
        {
            throw new MissingReferenceException("Actor stat/state is not set for " + this.name + ": " + this.GetEntityId());
        }

        if (buttonA.Equals(null) || buttonB.Equals(null) || buttonC.Equals(null) || buttonD.Equals(null))
        {
            throw new MissingReferenceException("Actor button configuration missing for " + this.name + ": " + this.GetEntityId());
        }

        if (abilityUp.Equals(null) || abilityDown.Equals(null) || abilityLeft.Equals(null) || abilityRight.Equals(null))
        {
            throw new MissingReferenceException("Actor ability configuration missing for " + this.name + ": " + this.GetEntityId());
        }
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

    public abstract Command HandleInput();

    public abstract void Move();

    public abstract void Jump();

    public abstract void Attack();

    public abstract void Block();

    public abstract void Grab();

    public ActorStat GetActorStat()
    {
        return this.actorStat;
    }
}
