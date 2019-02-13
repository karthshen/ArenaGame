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

    public MageWand wand;

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
        rb = GetComponent<Rigidbody>();

        ac = GetComponent<AnimatorController>();

        //Actor Config, button, ability, etc
        actorStat = new MageStat();
        CurrentHealth = this.actorStat.MaxHealth;
        CurrentEnergy = this.actorStat.MaxEnergy;
        entityName = actorName;

        abilityLeft = mageAbilityLeft;
        abilityRight = mageAbilityRight;
        abilityUp = mageAbilityUp;
        abilityDown = mageAbilityDown;

        deathAnimation = "animation,11";

        if (wand)
        {
            wand.ItemPickUp(this);
        }

        NullParameterCheck();
    }

    public override void Attack()
    {
        //Write code to capture the target enemy
        wand.UseItem();
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
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            state = new ActorDeathState();
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

    public override void Death()
    {
       
    }
}
