public class ArcherData : ActorData
{
    public ArcherData()
    {
        //stat
        this.setActorStat(100, 5, 7, 5, 100, 15, 15, 5, 2);

        //animation
        AttackAnimation1 = "animation,23";
        AttackAnimation2 = "animation,23";
        AttackAnimation3 = "animation,23";

        AbilityDownAnimation = "animation,23";
        AbilityHorizAnimation = "animation,32";
        AbilityUpAnimation = "animation,32";
        AbilityTriggerAnimation = "animation,32";

        DeathAnimation = "animation,10";

        //sound
        AttackSound1 = SoundManager.instance.arrow_attack1;
        AttackSound2 = SoundManager.instance.arrow_attack2;
        AttackSound3 = SoundManager.instance.arrow_attack2;

        AbilityUpSound = SoundManager.instance.chicken1;
        AbilityHorizSound = SoundManager.instance.trap;
        AbilityDownSound = SoundManager.instance.arrow_attack2;
        AbilityTriggerSound = SoundManager.instance.hookshoot;
    }
}
