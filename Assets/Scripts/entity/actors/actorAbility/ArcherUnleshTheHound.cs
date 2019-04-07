using UnityEngine;
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

        caster.CurrentEnergy -= AbilityCost;

        if (!IsHoundSummonable())
        {
            return;
        }

        hound = Object.Instantiate(Resources.Load("Chicken")) as GameObject;
        ArcherHound archerHound = hound.GetComponent<ArcherHound>();
        archerHound.Owner = caster;
        archerHound.ItemStart();
    }

    private bool IsHoundSummonable()
    {
        ArcherHound[] hounds = GameObject.FindObjectsOfType<ArcherHound>();
        if(hounds.Length >= MAX_NUM_HOUND)
        {
            return false;
        }
        return true;
    }
}
