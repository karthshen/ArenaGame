using UnityEngine;
using System.Collections;

public class WarriorCharge : Ability
{
    private const float CHARGE_FORCE = 1f;
    private const float CHARGE_SPEED = 660f;

    public WarriorCharge(AActor caster)
    {
        this.caster = caster;
        AbilityCost = 1;
        DragInAir = false;
    }

    public void AbilityExecuteRip()
    {
        GameObject initialBomb = Object.Instantiate(Resources.Load("InitialBomb")) as GameObject;

        WarriorInitialBomb iBomb = initialBomb.GetComponent<WarriorInitialBomb>();

        iBomb.Owner = caster;

        iBomb.ItemStart();
    }

    public override void AbilityExecute()
    {
        //Warrior Charged
        //Debug.Log("Warrior Charrrrrrged");

        //This code leaves a bug where after the charge, if the actor stops in air, he is considered in StandingState
        caster.CurrentEnergy -= AbilityCost;

        float yDirectionInRadian = caster.GetYDirectionInRadian();

        caster.ClearForceOnActor();

        Vector3 chargeMovement = new Vector3(CHARGE_FORCE * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        caster.GetRigidbody().useGravity = false;

        caster.Block();

        caster.GetRigidbody().AddForce(chargeMovement * CHARGE_SPEED);

        ((WarriorActor)(caster)).shield.UseItem(caster);

        base.AbilityExecute();
    }
}
