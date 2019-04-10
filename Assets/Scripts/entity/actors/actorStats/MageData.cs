public class MageData : ActorData
{
    public MageData()
    {
        //stats
        this.setActorStat(100, 5, 7, 5, 100, 5, 15, 20, 2);

        //animations
        AbilityDownAnimation = "animation,36";
        AbilityHorizAnimation = "animation,3";
        AbilityUpAnimation = "animation,35";
        DeathAnimation = "animation,11";

        //sound
        AttackSound1 = SoundManager.instance.staff_attack2;
        //AttackSound2 = SoundManager.instance.arrow_attack2;
        //AttackSound3 = SoundManager.instance.arrow_attack2;

        //AbilityUpSound = SoundManager.instance.thunder;
        AbilityHorizSound = SoundManager.instance.tornado;
        //AbilityDownSound = SoundManager.instance.fireball;
        //AbilityTriggerSound = SoundManager.instance.jump;
    }
}
