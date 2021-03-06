﻿using UnityEngine;
using System.Collections;

public class ArcherUnleshTheHound : Ability
{
    GameObject hound;

    private const int MAX_NUM_HOUND = 1;
    
    public ArcherUnleshTheHound(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsHoundSummonable() && caster.GetType() != typeof(Sandbag))
        {
            return;
        }

        caster.CurrentEnergy -= AbilityCost;

        hound = Object.Instantiate(Resources.Load("Chicken")) as GameObject;
        ArcherHound archerHound = hound.GetComponent<ArcherHound>();
        archerHound.Owner = caster;
        archerHound.ItemStart();
    }

    private bool IsHoundSummonable()
    {
        ArcherHound[] hounds = GameObject.FindObjectsOfType<ArcherHound>();

        int counter = 0;

        foreach(ArcherHound hound in hounds)
        {
            if(hound.Owner && hound.Owner.GetEntityId() == caster.GetEntityId())
            {
                counter++;
            }
        }

        if (counter >= MAX_NUM_HOUND)
            return false;

        return true;
    }
}
