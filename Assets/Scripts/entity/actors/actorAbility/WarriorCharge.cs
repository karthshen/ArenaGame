using UnityEngine;
using System.Collections;

public class WarriorCharge : Ability
{
    private const float CHARGE_FORCE = 1f;
    private const float CHARGE_SPEED = 300f;

    public WarriorCharge(AActor caster)
    {
        this.caster = caster;
    }

    public override void AbilityExecute()
    {
        //Warrior Charged
        //Debug.Log("Warrior Charrrrrrged");

        //This code leaves a bug where after the charge, if the actor stops in air, he is considered in StandingState
        float yDirectionInRadian = caster.transform.GetChild(0).rotation.eulerAngles.y * Mathf.PI / 180;

        Vector3 chargeMovement = new Vector3(CHARGE_FORCE * Mathf.Sin(yDirectionInRadian), 0f, 0f);

        caster.GetRigidbody().useGravity = false;

        caster.GetRigidbody().AddForce(chargeMovement * CHARGE_SPEED);
    }
}
