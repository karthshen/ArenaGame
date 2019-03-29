public class ArcherData : ActorData
{
    public ArcherData()
    {
        this.setActorStat(100, 5, 7, 5, 100, 15, 15, 5, 2);

        AttackAnimation1 = "animation,23";
        AttackAnimation2 = "animation,23";
        AttackAnimation3 = "animation,23";

        DeathAnimation = "animation,10";
    }
}
