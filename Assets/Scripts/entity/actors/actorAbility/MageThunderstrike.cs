using UnityEngine;
using System.Collections;

public class MageThunderstrike : Ability
{
    GameObject thunderstrike;

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

        thunderstrike = Object.Instantiate(Resources.Load("Thunderstrike")) as GameObject;

        Thunderstrike strike = thunderstrike.GetComponent<Thunderstrike>();
        strike.Owner = caster;
        strike.ItemStart();
    }
}
