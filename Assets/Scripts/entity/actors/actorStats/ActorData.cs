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
    private string movingAnimation = "animation,17";
    private string walkAnimation = "animation,25";
    private string runningAnimation = "animation,20";
    private string freezeAnimation = "animation,8";
    private string abilityDownAnimation;
    private string abilityHorizAnimation;
    private string abilityUpAnimation;
    private string abilityTriggerAnimation;
    private string abilityBumperAnimation;
    private string abilityNeutralAnimation;
    private string jumpAnimation;
    private string deathAnimation = "animation,10";
    private string idleAnimation = "animation,13";

    //Sound FX
    private AudioClip attackSound1;
    private AudioClip attackSound2;
    private AudioClip attackSound3;
    private AudioClip movingSound;
    private AudioClip walkingSound = SoundManager.instance.footstep;
    private AudioClip runningSound = SoundManager.instance.footstep;
    private AudioClip damagedSound;
    private AudioClip abilityDownSound;
    private AudioClip abilityUpSound;
    private AudioClip abilityHorizSound;
    private AudioClip abilityNeutralSound;
    private AudioClip abilityTriggerSound;
    private AudioClip jumpSound = SoundManager.instance.jump;
    private AudioClip landSound = SoundManager.instance.land;
    private AudioClip deathSound;
    private AudioClip idleSound;

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
        set
        {
            maxHealth = value;
        }

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

    public string MovingAnimation
    {
        get
        {
            return movingAnimation;
        }

        set
        {
            movingAnimation = value;
        }
    }

    public string RunningAnimation
    {
        get
        {
            return runningAnimation;
        }

        set
        {
            runningAnimation = value;
        }
    }

    public string WalkAnimation
    {
        get
        {
            return walkAnimation;
        }

        set
        {
            walkAnimation = value;
        }
    }

    public string AbilityTriggerAnimation
    {
        get
        {
            return abilityTriggerAnimation;
        }

        set
        {
            abilityTriggerAnimation = value;
        }
    }

    public AudioClip AttackSound1
    {
        get
        {
            return attackSound1;
        }

        set
        {
            attackSound1 = value;
        }
    }

    public AudioClip AttackSound2
    {
        get
        {
            return attackSound2;
        }

        set
        {
            attackSound2 = value;
        }
    }

    public AudioClip AttackSound3
    {
        get
        {
            return attackSound3;
        }

        set
        {
            attackSound3 = value;
        }
    }

    public AudioClip MovingSound
    {
        get
        {
            return movingSound;
        }

        set
        {
            movingSound = value;
        }
    }

    public AudioClip WalkingSound
    {
        get
        {
            return walkingSound;
        }

        set
        {
            walkingSound = value;
        }
    }

    public AudioClip RunningSound
    {
        get
        {
            return runningSound;
        }

        set
        {
            runningSound = value;
        }
    }

    public AudioClip AbilityDownSound
    {
        get
        {
            return abilityDownSound;
        }

        set
        {
            abilityDownSound = value;
        }
    }

    public AudioClip AbilityUpSound
    {
        get
        {
            return abilityUpSound;
        }

        set
        {
            abilityUpSound = value;
        }
    }

    public AudioClip AbilityHorizSound
    {
        get
        {
            return abilityHorizSound;
        }

        set
        {
            abilityHorizSound = value;
        }
    }

    public AudioClip AbilityTriggerSound
    {
        get
        {
            return abilityTriggerSound;
        }

        set
        {
            abilityTriggerSound = value;
        }
    }

    public AudioClip JumpSound
    {
        get
        {
            return jumpSound;
        }

        set
        {
            jumpSound = value;
        }
    }

    public AudioClip DeathSound
    {
        get
        {
            return deathSound;
        }

        set
        {
            deathSound = value;
        }
    }

    public AudioClip IdleSound
    {
        get
        {
            return idleSound;
        }

        set
        {
            idleSound = value;
        }
    }

    public AudioClip LandSound
    {
        get
        {
            return landSound;
        }

        set
        {
            landSound = value;
        }
    }

    public AudioClip DamagedSound
    {
        get
        {
            return damagedSound;
        }

        set
        {
            damagedSound = value;
        }
    }

    public string AbilityBumperAnimation
    {
        get
        {
            return abilityBumperAnimation;
        }

        set
        {
            abilityBumperAnimation = value;
        }
    }

    public string FreezeAnimation
    {
        get
        {
            return freezeAnimation;
        }

        set
        {
            freezeAnimation = value;
        }
    }

    public string AbilityNeutralAnimation
    {
        get
        {
            return abilityNeutralAnimation;
        }

        set
        {
            abilityNeutralAnimation = value;
        }
    }

    public AudioClip AbilityNeutralSound
    {
        get
        {
            return abilityNeutralSound;
        }

        set
        {
            abilityNeutralSound = value;
        }
    }
}
