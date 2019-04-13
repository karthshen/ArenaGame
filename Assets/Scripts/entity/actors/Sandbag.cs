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

        RespawnLives = 100;

        InitializeActor();

        NullParameterCheck();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Block()
    {
        throw new System.NotImplementedException();
    }

    public override void GenerateAirAttackQueue()
    {
        throw new System.NotImplementedException();
    }

    public override void GenerateAttackQueue()
    {
        throw new System.NotImplementedException();
    }

    public override void Grab()
    {
        throw new System.NotImplementedException();
    }

    public override void Unblock()
    {
        throw new System.NotImplementedException();
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
