using UnityEngine;

public class MageThunderbolt : Ability
{
    GameObject thunderbolt;

    public MageThunderbolt(AActor caster)
    {
        this.caster = caster;
    }

    public override void AbilityExecute()
    {
        thunderbolt = Object.Instantiate(Resources.Load("ThunderBolt")) as GameObject;
        Thunderbolt bolt = thunderbolt.GetComponent<Thunderbolt>();
        bolt.SetOwner(caster);
        bolt.ProjectileStart();
    }
}