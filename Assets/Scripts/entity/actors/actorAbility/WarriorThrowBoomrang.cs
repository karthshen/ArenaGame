﻿using UnityEngine;
using UnityEditor;

public class WarriorThrowBoomrang : Ability
{
    GameObject boomrang;

    private const int MAX_NUM_BOOMRANG = 1;

    public WarriorThrowBoomrang(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        if (!IsBoomrangThrowable())
            return;

        caster.CurrentEnergy -= AbilityCost;

        boomrang = Object.Instantiate(Resources.Load("Boomrang")) as GameObject;
        WarriorBoomrang warriorBoomrang = boomrang.GetComponent<WarriorBoomrang>();
        warriorBoomrang.SetOwner(caster);
        warriorBoomrang.ProjectileStart();
    }

    private bool IsBoomrangThrowable()
    {
        WarriorBoomrang[] boomrangs = GameObject.FindObjectsOfType<WarriorBoomrang>();

        if(boomrangs.Length >= MAX_NUM_BOOMRANG)
        {
            return false;
        }
        return true;
    }
}