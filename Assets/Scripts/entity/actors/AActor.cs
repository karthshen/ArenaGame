using UnityEngine;
using System.Collections;
using InControl;

public abstract class AActor : AEntity
{
    //Attributes
    private float currentHealth;
    private float currentEnergy;
    private float moveHorizontal;

    protected ActorStat actorStat;

    protected Ability abilityUp;
    protected Ability abilityDown;
    protected Ability abilityLeft;
    protected Ability abilityRight;

    protected PickupItem item = null;

    protected Rigidbody rb;

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

    public float MoveHorizontal
    {
        get
        {
            return moveHorizontal;
        }

        set
        {
            moveHorizontal = value;
        }
    }

    //Functiosn
    public abstract float TakeDamage(float damage);

    public void HandleInput(InputDevice inputDevice)
    {
        ActorState newState = ((ActorState)state).HandleInput(this, inputDevice);
        state = newState;
    }

    public virtual void Move()
    {
        //TODO Improve the movement code
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        transform.Translate(movement * actorStat.MoveVelocity * Time.deltaTime);
        //Debug.Log("Translate" + transform.position.x);

        //rb.AddForce(movement * actorStat.MoveVelocity);
    }

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
