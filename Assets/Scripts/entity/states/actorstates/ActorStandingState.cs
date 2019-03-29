using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();
    Command blockCommand = new BlockCommand();
    Command grabCommand = new GrabCommand();
    Command attackCommand = new AttackCommand();

    Command abilityDownCommand = new ActorDownAbilityCommand();
    Command abilityLeftCommand = new ActorLeftAbilityCommand();
    Command abilityRightCommand = new ActorRightAbilityCommand();
    Command abilityUpCommand = new ActorAbilityUpCommand();

    public ActorStandingState() : base()
    {

    }

    public ActorStandingState(string previousState) : base(previousState)
    {

    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        //actor.GetAnimator().enabled = true;
        ActorStandingInitialize(actor);

        PlayAnimation(actor);

        if (actor.IsGrounded == false)
        {
            return new ActorJumpState();
        }

        if (inputDevice.LeftStickX.Value != 0 && GetType() != typeof(ActorMovingState))
        {
            return new ActorMovingState();
        }

        if ((inputDevice.Action3 || inputDevice.Action4))
        {
            actor.IsGrounded = false;
            jumpCommand.Execute(actor);
            ActorJumpState state = new ActorJumpState();
            state.JumpNum--;
            state.PlayStateAnimation(actor);
            return state;
        }
        else if (inputDevice.LeftTrigger || inputDevice.LeftBumper)
        {
            blockCommand.Execute(actor);
            return new ActorBlockState();
        }
        else if (inputDevice.RightTrigger || inputDevice.RightBumper)
        {
            grabCommand.Execute(actor);
            return new ActorGrabbingState();
        }
        else if (inputDevice.Action2 && actor.AttackTimer < AActor.ATTACK_INTERVAL)
        {
            actor.GenerateAttackQueue();
            //Debug.Log(actor.GetName() + " attacking from standing state");
            ActorState state = new ActorAttackState();
            //state.HandleInput(actor, inputDevice);
            return state;
        }
        //Ability Left Input
        else if ((inputDevice.Action1.WasPressed && inputDevice.LeftStickX.Value < (0 - Mathf.Epsilon)) && actor.CurrentEnergy >= actor.abilityLeft.AbilityCost)
        {
            actor.CastTimer = AActor.CAST_DURATION;
            abilityLeftCommand.Execute(actor);
            return new ActorHoriAbilityState();
        }
        //Ability Right Input
        else if ((inputDevice.Action1.WasPressed && inputDevice.LeftStickX.Value > (0 + Mathf.Epsilon)) && actor.CurrentEnergy >= actor.abilityRight.AbilityCost)
        {
            actor.CastTimer = AActor.CAST_DURATION;
            abilityRightCommand.Execute(actor);
            return new ActorHoriAbilityState();
        }
        //Ability Down Input
        else if ((inputDevice.Action1.WasPressed) && actor.CurrentEnergy >= actor.abilityDown.AbilityCost)
        {
            actor.CastTimer = AActor.CAST_DURATION;
            abilityDownCommand.Execute(actor);
            return new ActorDownAbilityState();
        }

        return this;
    }

    private void ActorStandingInitialize(AActor actor)
    {
        if (actor.GetRigidbody().useGravity == false)
        {
            actor.GetRigidbody().useGravity = true;
        }

        if (actor.BIsBlocking)
            actor.Unblock();
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt(actor.GetActorStat().IdleAnimation);
    }
}