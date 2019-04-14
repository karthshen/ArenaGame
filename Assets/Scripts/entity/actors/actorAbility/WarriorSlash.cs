using UnityEngine;
using System.Collections;

public class WarriorSlash : Ability
{
    GameObject calibur;

    public WarriorSlash(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;
        calibur = Object.Instantiate(Resources.Load("Excalibur")) as GameObject;

        Excalibur light = calibur.GetComponent<Excalibur>();
        light.Owner = caster;
        light.ItemStart();
    }
}
