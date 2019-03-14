using UnityEngine;
using System.Collections;
using InControl;

public abstract class ActorState : EntityState
{
    protected ActorState()
    {
        //Debug.Log("Entering State: " + GetType());
    }

    public abstract ActorState HandleInput(AActor actor, InputDevice inputDevice);

    protected abstract void PlayAnimation(AActor actor);

    //This is used by outside clients
    public void PlayStateAnimation(AActor actor)
    {
        PlayAnimation(actor);
    }
}
