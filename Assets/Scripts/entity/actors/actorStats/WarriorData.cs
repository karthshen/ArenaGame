public class WarriorData : ActorData
{
    public WarriorData()
    {
        //Stat
        this.setActorStat(100, 5, 7, 5, 100, 15, 20, 5, 4);

        //Animation
        AbilityHorizAnimation = "animation,35";
        AbilityDownAnimation = "animation,5";
        AbilityUpAnimation = "animation,6";
        DeathAnimation = "animation,10";
        AbilityTriggerAnimation = "animation,32";
        AbilityNeutralAnimation = "animation,9";

        //Sound
        AttackSound1 = SoundManager.instance.sword_attack1;
        AttackSound2 = SoundManager.instance.sword_attack2;
        AttackSound3 = SoundManager.instance.sword_attack1;

        AbilityUpSound = SoundManager.instance.sword_attack3;
        AbilityDownSound = SoundManager.instance.sword_attack3;
        AbilityTriggerSound = SoundManager.instance.hookshoot;
        AbilityNeutralSound = SoundManager.instance.shieldslam;
    }
}
