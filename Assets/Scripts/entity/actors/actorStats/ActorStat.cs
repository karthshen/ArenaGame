using UnityEngine;
using System.Collections;

public abstract class ActorStat
{
    private float maxHealth;
    private float maxEnergy;
    private float moveVelocity;
    private float jumpVelocity;
    private float sprintVelocity;
    private float attackPower;
    private float defensePower;
    private float abiltiyPower;
    private float damageMultiplier = 1;
    private float defenseMultiplier = 1;
    private float velocityMultiplier = 1;

    protected void setActorStat(
        float maxHealth, float maxEnergy, float moveVelocity, float jumpVelocity, float sprintVelocity,
        float attackPower, float defensePower, float abilityPower
        )
    {
        this.maxHealth = maxHealth;
        this.maxEnergy = maxEnergy;
        this.moveVelocity = moveVelocity;
        this.jumpVelocity = jumpVelocity;
        this.sprintVelocity = sprintVelocity;
        this.attackPower = attackPower;
        this.defensePower = defensePower;
        this.abiltiyPower = abilityPower;
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public float MaxEnergy
    {
        get
        {
            return maxEnergy;
        }
    }

    public float MoveVelocity
    {
        get
        {
            return moveVelocity;
        }
    }

    public float JumpVelocity
    {
        get
        {
            return jumpVelocity;
        }
    }

    public float SprintVelocity
    {
        get
        {
            return sprintVelocity;
        }
    }

    public float AttackPower
    {
        get
        {
            return attackPower;
        }
    }

    public float DefensePower
    {
        get
        {
            return defensePower;
        }
    }

    public float AbiltiyPower
    {
        get
        {
            return abiltiyPower;
        }
    }

    public float DamageMultiplier
    {
        get
        {
            return damageMultiplier;
        }

        set
        {
            damageMultiplier = value;
        }
    }

    public float DefenseMultiplier
    {
        get
        {
            return defenseMultiplier;
        }

        set
        {
            defenseMultiplier = value;
        }
    }

    public float VelocityMultiplier
    {
        get
        {
            return velocityMultiplier;
        }

        set
        {
            velocityMultiplier = value;
        }
    }
}
