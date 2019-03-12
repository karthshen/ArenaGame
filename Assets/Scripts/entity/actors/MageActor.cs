using UnityEngine;
using System.Collections;

public class MageActor : AActor
{
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
        mageMesh = new Mesh();
        entityMesh = mageMesh;
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimatorController>();

        //Actor Config, button, ability, etc
        actorStat = new MageStat();
        entityName = actorName;

        mageAbilityDown = new MageThunderbolt(this);

        abilityLeft = mageAbilityLeft;
        abilityRight = mageAbilityRight;
        abilityUp = mageAbilityUp;
        abilityDown = mageAbilityDown;

        deathAnimation = "animation,11";

        if (wand)
        {
            wand.ItemPickUp(this);
        }

        InitializeActor();

        NullParameterCheck();
    }

    public override void Attack()
    {
        //Write code to capture the target enemy
        wand.UseItem();
    }

    public override void Block()
    {
        BIsBlocking = true;
    }

    public override void Unblock()
    {
        BIsBlocking = false;
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

    public override void GenerateAirAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Attack1);
    }
}
