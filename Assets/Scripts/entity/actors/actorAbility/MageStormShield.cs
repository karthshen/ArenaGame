﻿using UnityEngine;
using System.Collections;

public class MageStormShield : Ability
{
    GameObject stormShield;

    public MageStormShield(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 3;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;
        stormShield = Object.Instantiate(Resources.Load("StormShield")) as GameObject;

        StormShield storm = stormShield.GetComponent<StormShield>();
        storm.Owner = caster;
        storm.ItemStart();
    }
}
