using UnityEngine;
using System.Collections;

public class Sandbag : AActor
{
    public Sandbag() : base()
    {

    }

    private void Start()
    {
        entityMesh = new Mesh();
        entityId = System.Guid.NewGuid();
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AnimatorController>();
        effectSource = GetComponent<AudioSource>();

        //Actor Config
        actorStat = new SandbagData();
        entityName = "Sandbag";

        abilityDown = new ArcherUnleshTheHound(this);

        RespawnLives = 100;

        InitializeActor();

        NullParameterCheck();
    }

    public override void Attack()
    {
        
    }

    public override void Block()
    {
        
    }

    public override void GenerateAirAttackQueue()
    {
        
    }

    public override void GenerateAttackQueue()
    {
        
    }

    public override void Grab()
    {
       
    }

    public override void Unblock()
    {
        
    }

    public override float TakeDamage(float damage, AActor attacker)
    {
        //damage = 1;
        FreezeTimer = 0f;
        state = new ActorFreezeState(0.3f, this, attacker);
        ((ActorFreezeState)state).PlayStateAnimation(this);

        return CurrentHealth;

    }

    private void Update()
    { 
        base.ActorUpdate();

        //((ActorState)(state)).PlayStateAnimation(this);
    }
}
