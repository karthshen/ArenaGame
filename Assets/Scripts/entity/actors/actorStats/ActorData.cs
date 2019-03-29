using UnityEngine;
using System.Collections;

public abstract class ActorData
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
    private int energyRegenerationTime;

    //Animation
    private string attackAnimation1 = "animation,3";
    private string attackAnimation2 = "animation,4";
    private string attackAnimation3 = "animation,2";
    private string abilityDownAnimation;
    private string abilityHorizAnimation;
    private string abilityUpAnimation;
    private string jumpAnimation;
    private string deathAnimation;
    private string idleAnimation = "animation,13";

    protected void setActorStat(
        float maxHealth, float maxEnergy, float moveVelocity, float jumpVelocity, float sprintVelocity,
        float attackPower, float defensePower, float abilityPower, int energyGenerationSpeed
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
        this.energyRegenerationTime = energyGenerationSpeed;
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

    public int EnergyRegenerationTime
    {
        get
        {
            return energyRegenerationTime;
        }

        set
        {
            energyRegenerationTime = value;
        }
    }

    public string AttackAnimation1
    {
        get
        {
            return attackAnimation1;
        }

        set
        {
            attackAnimation1 = value;
        }
    }

    public string AttackAnimation2
    {
        get
        {
            return attackAnimation2;
        }

        set
        {
            attackAnimation2 = value;
        }
    }

    public string AttackAnimation3
    {
        get
        {
            return attackAnimation3;
        }

        set
        {
            attackAnimation3 = value;
        }
    }

    public string AbilityDownAnimation
    {
        get
        {
            return abilityDownAnimation;
        }

        set
        {
            abilityDownAnimation = value;
        }
    }

    public string AbilityUpAnimation
    {
        get
        {
            return abilityUpAnimation;
        }

        set
        {
            abilityUpAnimation = value;
        }
    }

    public string JumpAnimation
    {
        get
        {
            return jumpAnimation;
        }

        set
        {
            jumpAnimation = value;
        }
    }

    public string IdleAnimation
    {
        get
        {
            return idleAnimation;
        }

        set
        {
            idleAnimation = value;
        }
    }

    public string DeathAnimation
    {
        get
        {
            return deathAnimation;
        }

        set
        {
            deathAnimation = value;
        }
    }

    public string AbilityHorizAnimation
    {
        get
        {
            return abilityHorizAnimation;
        }

        set
        {
            abilityHorizAnimation = value;
        }
    }
}
