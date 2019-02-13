using UnityEngine;
using System.Collections;

public class AttackCommand : Command
{
    public override void Execute(AActor actor)
    {
        actor.Attack();
    }
}
