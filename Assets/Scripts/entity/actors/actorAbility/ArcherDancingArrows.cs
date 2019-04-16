using UnityEngine;
using System.Collections;

public class ArcherDancingArrows : Ability
{
    GameObject dancingArrows;

    public ArcherDancingArrows(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 2;
    }

    public override void AbilityExecute()
    {
        base.AbilityExecute();

        caster.CurrentEnergy -= AbilityCost;

        dancingArrows = Object.Instantiate(Resources.Load("DancingArrows")) as GameObject;

        float moveHorizontal = 0.6f;

        dancingArrows.transform.GetChild(0).rotation = caster.transform.GetChild(0).rotation;

        float yDirectionInRadian = dancingArrows.transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180; ;

        dancingArrows.transform.position = new Vector3(caster.transform.position.x + -moveHorizontal * Mathf.Sin(yDirectionInRadian),
            caster.transform.position.y + caster.transform.lossyScale.y / 2, caster.transform.position.z);

        dancingArrows.GetComponent<DancingArrows>().SetOwner(caster);

        dancingArrows.GetComponent<DancingArrows>().ProjectileStart();
    }
}
