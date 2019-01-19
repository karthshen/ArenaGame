using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using InControl;

public class PlayerTest
{
    private class TestActor : AActor
    {
        public bool hasMoved = false;
        public bool hasJumped = false;

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

        public override void Jump()
        {
            hasJumped = true;
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

    TestActor testActor;
    InputDevice inputDevice;

    [SetUp]
    public void Setup()
    {
        testActor = new TestActor();
        inputDevice = new InputDevice("");
        inputDevice.TestControl();
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
        inputDevice.UpdateWithStateTestCase(InputControlType.Action1, true, 1000);

        testActor.HandleInput(inputDevice);

        //Character should be moved, and right after the move, the character should be stopped and Standing still
        Assert.AreEqual(true, testActor.hasMoved);
        Assert.AreEqual(testActor.GetState().GetType(), typeof(ActorMovingState));

        testActor.HandleInput(inputDevice);
        Assert.AreEqual(testActor.GetState().GetType(), typeof(ActorStandingState));
    }
   
    [Test]
    public void PlayerJumpTest()
    {
        testActor.HandleInput(inputDevice);
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
