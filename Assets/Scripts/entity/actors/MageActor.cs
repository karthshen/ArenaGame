using UnityEngine;
using System.Collections;

public class MageActor : AActor
{
    Mesh mageMesh;

    Ability mageAbilityUp;
    Ability mageAbilityDown;
    Ability mageAbilityLeft;
    Ability mageAbilityRight;
    Ability mageAbilityTrigger;
    Ability mageAbilityBumper;

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
        effectSource = GetComponent<AudioSource>();

        //Actor Config, button, ability, etc
        actorStat = new MageData();
        entityName = actorName;

        mageAbilityDown = new MageThunderbolt(this);
        mageAbilityLeft = new MageStormShield(this);
        mageAbilityRight = new MageStormShield(this);
        mageAbilityUp = new MageThunderstrike(this);
        mageAbilityTrigger = new MageThrowTeleportBolt(this);
        mageAbilityBumper = new MageTeleport(this);

        abilityLeft = mageAbilityLeft;
        abilityRight = mageAbilityRight;
        abilityUp = mageAbilityUp;
        abilityDown = mageAbilityDown;
        abilityTrigger = mageAbilityTrigger;
        abilityBumper = mageAbilityBumper;

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
        wand.UseItem(this);
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
        //attackQueue.Enqueue(Combo.Attack1);
        attackQueue.Enqueue(Combo.Null);
    }

    public override void GenerateAirAttackQueue()
    {
        attackQueue.Clear();

        attackQueue.Enqueue(Combo.Attack0);
        attackQueue.Enqueue(Combo.Attack1);
    }

    public override float TakeDamage(float damage, AActor attacker)
    {
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
}
