﻿using UnityEngine;
using System.Collections;

public class AttackCommand : Command
{
    public override void Execute(AActor actor)
    {
        //Refresh AttackCode
        actor.AttackCode = System.Guid.NewGuid();

        //Debug.Log("Popping Attack Stack");
        actor.AttackTimer = AActor.ATTACK_TIMER;
        actor.Attack();
        actor.attackQueue.Dequeue();
    }
}
