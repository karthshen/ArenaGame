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

    public ActorStandingState() :base()
    {
       
    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        //actor.GetAnimator().enabled = true;
        PlayAnimation(actor);
        if (inputDevice.LeftStickX.Value != 0 && GetType()!=typeof(ActorMovingState))
        {
            return new ActorMovingState();
        }

        if ((inputDevice.Action3 || inputDevice.Action4))
        {
            actor.IsGrounded = false;
            jumpCommand.Execute(actor);
            return new ActorJumpState();
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
        else if (inputDevice.Action2)
        {
            actor.GenerateAttackQueue();
            actor.AttackTimer = AActor.ATTACK_TIMER;
            attackCommand.Execute(actor);
            Debug.Log(actor.GetName() + " attacking from standing state");
            return new ActorAttackState();
        }

        return this;
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,13");
    }
}