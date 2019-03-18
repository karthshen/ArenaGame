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
            //Refresh Attack Code
            actor.AttackCode = System.Guid.NewGuid();

            actor.GenerateAttackQueue();
            
            actor.AttackTimer = AActor.ATTACK_TIMER;
            attackCommand.Execute(actor);
            //Debug.Log(actor.GetName() + " attacking from standing state");
            return new ActorAttackState();
        }

        else if (inputDevice.Action1.WasPressed && actor.CurrentEnergy >= actor.abilityDown.AbilityCost)
        {
            actor.CastTimer = AActor.CAST_DURATION;
            abilityDownCommand.Execute(actor);
            return new ActorAbilityState();
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
        actor.GetAnimatorController().SetInt("animation,13");
    }
}