﻿using UnityEngine;

public class MageThunderbolt : Ability
{
    GameObject thunderbolt;

    public MageThunderbolt(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;

        thunderbolt = Object.Instantiate(Resources.Load("Fireball")) as GameObject;
        Thunderbolt bolt = thunderbolt.GetComponent<Thunderbolt>();
        bolt.SetOwner(caster);
        bolt.ProjectileStart();
    }
}