using UnityEngine;
using UnityEditor;
using InControl;

public class ActorFreezeState : ActorState
{
    //Stunned time
    float time = 0f;
    float knockingForce = 200f;

    AActor attacker;

    public ActorFreezeState(float time, AActor actor, AActor attacker)
    {
        this.time = time;
        this.attacker = attacker;
        HandleInput(actor, null);
    }

    public override ActorState HandleInput(AActor actor, InputDevice inputDevice)
    {
        PlayAnimation(actor);

        if(actor.FreezeTimer == 0f)
        {
            actor.FreezeTimer = time;

            //Knocking back
            float yDirectionInRadian = attacker.transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

            Vector3 backMovement = new Vector3(knockingForce * Mathf.Sin(yDirectionInRadian), 0f, 0f);

            actor.GetRigidbody().AddForce(backMovement);
        }

        return this;
        
    }

    protected override void PlayAnimation(AActor actor)
    {
        actor.GetAnimatorController().SetInt("animation,8");
    }
}