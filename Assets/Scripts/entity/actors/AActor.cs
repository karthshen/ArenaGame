using UnityEngine;
using System.Collections;

public abstract class AActor : AEntity
{
    //Attributes
    private float currentHealth;
    private float currentEnergy;

    private ActorStat actorStat;

    protected Command buttonA;
    protected Command buttonB;
    protected Command buttonC;
    protected Command buttonD;

    protected Ability abilityUp;
    protected Ability abilityDown;
    protected Ability abilityLeft;
    protected Ability abilityRight;

    protected new ActorState state;
    protected PickupItem item = null;

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

    public void Move();

    public abstract void Jump();

    public abstract void Attack();

    public abstract void Block();

    public abstract void Grab();

    public ActorStat GetActorStat()
    {
        return this.actorStat;
    }
}
