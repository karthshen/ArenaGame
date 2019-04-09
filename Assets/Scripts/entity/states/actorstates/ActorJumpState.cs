using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    Command jumpCommand = new JumpCommand();
    Command moveCommand = new MoveCommand();
    Command attackCommand = new AttackCommand();

    Command abilityDownCommand = new ActorDownAbilityCommand();
    Command abilityLeftCommand = new ActorLeftAbilityCommand();
    Command abilityRightCommand = new ActorRightAbilityCommand();
    Command abilityUpCommand = new ActorAbilityUpCommand();

    int jumpNum = 2;
    bool bAttacked = false;
    bool bAbilityCasted = false;

    public ActorJumpState()
    {
    }

    public int JumpNum
    {
        get
        {
            return jumpNum;
        }

        set
        {
            jumpNum = value;
        }
    }

    public bool BAttacked
    {
        get
        {
            return bAttacked;
        }

        set
        {
            bAttacked = value;
        }
    }

    public bool BAbilityCasted
    {
        get
        {
            return bAbilityCasted;
        }

        set
        {
            bAbilityCasted = value;
        }
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        if (inputDevice.LeftStickX.Value != 0)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        if (inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed)
        {
            if (JumpNum > 0 && actor.IsGrounded == false)
            {
                Jumped();
                actor.JumpNum = jumpNum;
                PlayAnimation(actor);
                //Debug.Log("Jump: "+ jumpNum + "- Vertical Velocity: " + actor.GetRigidbody().velocity.y);
                jumpCommand.Execute(actor);
                return this;
            }
        }

        if (inputDevice.Action2 && !BAttacked)
        {
            actor.GenerateAirAttackQueue();
            return new ActorAirAttackState();
        }
        else if (actor.AbilityCastedInAir)
        {
            return this;
        }
        //Ability Up Input
        else if((inputDevice.Action1.WasPressed && inputDevice.LeftStickY.Value > 0.6f) && actor.abilityUp.CanCastInAir == true)
        {
            if (actor.CurrentEnergy >= actor.abilityUp.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                abilityUpCommand.Execute(actor);
                return new ActorUpAbilityState();
            }
        }
        //Ability Left Input
        else if ((inputDevice.Action1.WasPressed && inputDevice.LeftStickX.Value < (0 - Mathf.Epsilon)) && actor.abilityLeft.CanCastInAir == true)
        {
            if (actor.CurrentEnergy >= actor.abilityLeft.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                abilityLeftCommand.Execute(actor);
                return new ActorHoriAbilityState();
            }
        }
        //Ability Right Input
        else if ((inputDevice.Action1.WasPressed && inputDevice.LeftStickX.Value > (0 + Mathf.Epsilon)) && actor.abilityRight.CanCastInAir == true)
        {
            if (actor.CurrentEnergy >= actor.abilityRight.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                abilityRightCommand.Execute(actor);
                return new ActorHoriAbilityState();
            }
        }
        //Ability Down Input
        else if ((inputDevice.Action1.WasPressed) && actor.abilityDown.CanCastInAir == true)
        {
            if (actor.CurrentEnergy >= actor.abilityDown.AbilityCost)
            {
                actor.CastTimer = AActor.CAST_DURATION;
                abilityDownCommand.Execute(actor);
                return new ActorDownAbilityState();
            }
        }

        return this;
    }

    public void Jumped()
    {
        JumpNum--;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if (JumpNum == 1)
        {
            actor.GetAnimatorController().SetInt("animation,16");
        }
        else if (JumpNum == 0)
        {
            actor.GetAnimatorController().SetInt("animation,4");
        }
        else
        {
            actor.GetAnimatorController().SetInt("animation,16");
        }
    }
}
