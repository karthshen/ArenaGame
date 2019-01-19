using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerTest
{
    private class TestActor : AActor
    {
        public bool hasMoved = false;

        //Everything copied from WarriorActor
        ActorState defaultState;
        Mesh warriorMesh = new Mesh();

        Ability warriorAbilityUp;
        Ability warriorAbilityDown;
        Ability warriorAbilityLeft;
        Ability warriorAbilityRight;

        string actorName = "Warrior";

        public TestActor() : base()
    {
            //Entity Config
            entityId = System.Guid.NewGuid();
            defaultState = new ActorStandingState();
            state = defaultState;
            entityMesh = warriorMesh;
            entityName = actorName;

            //Actor Config, button, ability, etc
            actorStat = new WarriorStat();

            abilityLeft = warriorAbilityLeft;
            abilityRight = warriorAbilityRight;
            abilityUp = warriorAbilityUp;
            abilityDown = warriorAbilityDown;

            NullParameterCheck();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Block()
        {
            throw new System.NotImplementedException();
        }

        public override void Grab()
        {
            throw new System.NotImplementedException();
        }

        public override void HandleInput()
        {
            ActorState newState = ((ActorState)state).HandleInput(this);
            if (!state.Equals(null))
            {
                state = newState;
            }
        }

        public override void Jump()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            hasMoved = true;
        }

        public override float TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }

    [Test]
    public void PlayerNullAttributeTest()
    {
        ArcherActor archer = new ArcherActor();
        WarriorActor warrior = new WarriorActor();
        MageActor mage = new MageActor();

        Assert.AreEqual(archer.GetName(), "Archer");
        Assert.AreEqual(warrior.GetName(), "Warrior");
        Assert.AreEqual(mage.GetName(), "Mage");
    }

    [Test]
    public void PlayerMoveTest()
    {
        TestActor testActor = new TestActor();
        ((ActorState)testActor.GetState()).HandleInput(testActor);

        //Character should be moved, and right after the move, the character should be stopped and Standing still
        Assert.AreEqual(true, testActor.hasMoved);
        Assert.AreEqual(testActor.GetState().GetType(), typeof(ActorStandingState));
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PlayerMoveTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
