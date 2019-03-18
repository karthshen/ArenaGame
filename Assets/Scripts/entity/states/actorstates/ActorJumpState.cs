using UnityEngine;
using System.Collections;
using InControl;

public class ActorJumpState : ActorState
{
    Command jumpCommand = new JumpCommand();
    Command moveCommand = new MoveCommand();
    Command attackCommand = new AttackCommand();

    int jumpNum = 2;
    bool bAttacked = false;

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

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        if (inputDevice.LeftStickX.Value != 0 && !bAttacked)
        {
            actor.MoveHorizontal = inputDevice.LeftStickX.Value;
            moveCommand.Execute(actor);
        }

        if (inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed)
        {
            if (JumpNum > 0 && actor.IsGrounded == false)
            {
                Jumped();
                //Debug.Log("Jump: "+ jumpNum + "- Vertical Velocity: " + actor.GetRigidbody().velocity.y);
                jumpCommand.Execute(actor);
                return this;
            }
        }

        if (inputDevice.Action2 && !BAttacked)
        {
            //Refresh AttackCode
            actor.AttackCode = System.Guid.NewGuid();

            actor.GenerateAirAttackQueue();
            actor.GetRigidbody().drag = AActor.AIRBORNE_DRAG;
            actor.AttackTimer = AActor.ATTACK_TIMER;
            attackCommand.Execute(actor);
            //Debug.Log(actor.GetName() + " attacking from standing state");
            return new ActorAirAttackState();

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
        else
        {
            actor.GetAnimatorController().SetInt("animation,4");
        }
    }
}
