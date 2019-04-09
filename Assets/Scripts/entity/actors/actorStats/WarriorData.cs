public class WarriorData : ActorData
{
    public WarriorData()
    {
        //Stat
        this.setActorStat(100, 5, 7, 5, 100, 15, 20, 5, 4);

        //Animation
        AbilityHorizAnimation = "animation,9";
        AbilityDownAnimation = "animation,5";
        AbilityUpAnimation = "animation,35";
        DeathAnimation = "animation,10";

        //Sound
        AttackSound1 = SoundManager.instance.sword_attack1;
        AttackSound2 = SoundManager.instance.sword_attack2;
        AttackSound3 = SoundManager.instance.sword_attack3;

        //AbilityUpSound
        AbilityDownSound = AttackSound1;
        AbilityHorizSound = SoundManager.instance.arrow_attack1;
        //AbilityTriggerSound
    }
}
