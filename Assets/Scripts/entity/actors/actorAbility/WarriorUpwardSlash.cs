using UnityEngine;
using System.Collections;

public class WarriorUpwardSlash : Ability
{
    GameObject calibur;

    public WarriorUpwardSlash(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;
        calibur = Object.Instantiate(Resources.Load("UpwardSlash")) as GameObject;

        UpwardCalibur light = calibur.GetComponent<UpwardCalibur>();
        light.SetOwner(caster);
        light.ProjectileStart();
    }
}
