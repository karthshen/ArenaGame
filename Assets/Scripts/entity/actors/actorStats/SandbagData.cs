using UnityEngine;
using System.Collections;

public class SandbagData : ActorData
{
    public SandbagData()
    {
        //Stat
        this.setActorStat(100, 100, 7, 5, 100, 15, 20, 5, 4);

        FreezeAnimation = "animation,15";
        IdleAnimation = "animation,17";
        JumpAnimation = "animation,16";

        AbilityDownAnimation = "animation,7";
        AbilityUpAnimation = "animation,7";
        AbilityHorizAnimation = "animation,7"; 
        AbilityTriggerAnimation = "animation,7";
        AbilityBumperAnimation = "animation,7";

        DamagedSound = SoundManager.instance.chicken1;
    }
}
