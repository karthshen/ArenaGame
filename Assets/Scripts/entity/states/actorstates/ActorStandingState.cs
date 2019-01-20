using UnityEngine;
using UnityEditor;
using InControl;

public class ActorStandingState : ActorState
{
    Command moveCommand = new MoveCommand();
    Command jumpCommand = new JumpCommand();
    Command blockCommand = new BlockCommand();
    Command grabCommand = new GrabCommand();

    public ActorStandingState()
    {
       
    }


    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        //actor.GetAnimator().enabled = true;
        actor.GetAnimator().SetInteger("animation", 13);

        if (inputDevice.LeftStickX.Value != 0 && GetType()!=typeof(ActorMovingState))
        {
            Debug.Log("Creating Moving State" + Time.frameCount);
            actor.GetAnimator().StopPlayback();
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

        return this;
    }
}