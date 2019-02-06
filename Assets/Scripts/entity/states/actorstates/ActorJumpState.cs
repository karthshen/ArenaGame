using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    Command jumpCommand = new JumpCommand();
    Command moveCommand = new MoveCommand();

    int jumpNum = 1;

    public ActorJumpState()
    {
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        if (inputDevice.LeftStickX.Value != 0)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        if (inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed)
        {
            if (jumpNum > 0 && actor.IsGrounded == false)
            {
                Jumped();
                //Debug.Log("Jump: "+ jumpNum + "- Vertical Velocity: " + actor.GetRigidbody().velocity.y);
                jumpCommand.Execute(actor);
                return this;
            }
        }

        return this;
    }

    public void Jumped()
    {
        jumpNum--;
    }

    protected override void PlayAnimation(AActor actor)
    {
        if (jumpNum == 1)
        {
            actor.GetAnimatorController().SetInt("animation,16");
        }
        else
        {
            actor.GetAnimatorController().SetInt("animation,4");
        }
    }
}
