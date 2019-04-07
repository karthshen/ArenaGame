using UnityEngine;
using System.Collections;

public class MageThunderstrike : Ability
{
    GameObject thundercloud;

    public MageThunderstrike(AActor caster)
    {
        this.caster = caster;
        CanCastInAir = false;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;

        thundercloud = Object.Instantiate(Resources.Load("Thundercloud")) as GameObject;

        Thundercloud strike = thundercloud.GetComponent<Thundercloud>();
        strike.Owner = caster;
        strike.ItemStart();
    }
}
