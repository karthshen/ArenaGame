using UnityEngine;
using System.Collections;

public class SandbagData : ActorData
{
    public SandbagData()
    {
        //Stat
        this.setActorStat(100, 5, 7, 5, 100, 15, 20, 5, 4);

        FreezeAnimation = "animation,15";
        IdleAnimation = "animation,6";
        JumpAnimation = "animation,2";

        DamagedSound = SoundManager.instance.chicken1;
    }
}
