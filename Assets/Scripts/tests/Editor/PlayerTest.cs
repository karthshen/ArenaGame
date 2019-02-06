﻿using UnityEngine;
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
        public bool hasBlocked = false;
        public int hasJumped = 0;

        //Everything copied from WarriorActor
        ActorState defaultState;
        Mesh warriorMesh;

        Ability warriorAbilityUp;
        Ability warriorAbilityDown;
        Ability warriorAbilityLeft;
        Ability warriorAbilityRight;

        string actorName = "Warrior";

        public TestActor() : base()
        {
            
        }

        private void Start()
        {
            //Entity Config
            entityId = System.Guid.NewGuid();
            defaultState = new ActorStandingState();
            state = defaultState;
            entityMesh = warriorMesh;
            rb = GetComponent<Rigidbody>();
            ac = GetComponent<AnimatorController>();

            //Actor Config, button, ability, etc
            actorStat = new WarriorStat();
            entityName = actorName;

            abilityLeft = warriorAbilityLeft;
            abilityRight = warriorAbilityRight;
            abilityUp = warriorAbilityUp;
            abilityDown = warriorAbilityDown;

            NullParameterCheck();
        }

       /*
        * This is for test cases ONLY
        */
        public void CallStart()
        {
            Start();
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Block()
        {
            hasBlocked = true;
        }

        public override void Grab()
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            hasJumped++;
        }

        public override void Move()
        {
            base.Move();
            hasMoved = true;
        }

        public override float TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }

    GameObject testActor;
    GameObject warriorActor;
    GameObject archerActor;
    GameObject mageActor;

    GameObject warriorMesh;

    InputDevice inputDevice;

    [SetUp]
    public void Setup()
    {
        testActor = new GameObject("TestActor");
        warriorActor = new GameObject("WarriorActor");
        archerActor = new GameObject("ArcherActor");
        mageActor = new GameObject("MageActor");
        warriorMesh = new GameObject("WarriorMesh");

        testActor.AddComponent<TestActor>();
        testActor.AddComponent<Rigidbody>();
        testActor.AddComponent<AnimatorController>();
        testActor.AddComponent<Animator>();

        //Attach Warrior Mesh to TestActor
        warriorMesh.transform.parent = testActor.transform;

        //Setup animators
        Animator[] animators = new Animator[1];
        animators[0] = testActor.GetComponent<Animator>();
        testActor.GetComponent<AnimatorController>().animators = animators;

        testActor.GetComponent<TestActor>().CallStart();

        inputDevice = new InputDevice("");
        inputDevice.TestControl();
    }

    [Test]
    public void PlayerNullAttributeTest()
    {
        warriorActor.AddComponent<WarriorActor>();
        warriorActor.GetComponent<WarriorActor>().CallStart();

        archerActor.AddComponent<ArcherActor>();
        archerActor.GetComponent<ArcherActor>().CallStart();

        mageActor.AddComponent<MageActor>();
        mageActor.GetComponent<MageActor>().CallStart();

        Assert.AreEqual(archerActor.GetComponent<ArcherActor>().GetName(), "Archer");
        Assert.AreEqual(warriorActor.GetComponent<WarriorActor>().GetName(), "Warrior");
        Assert.AreEqual(mageActor.GetComponent<MageActor>().GetName(), "Mage");

        Assert.AreNotEqual(archerActor.GetComponent<ArcherActor>().GetActorStat().MoveVelocity, warriorActor.GetComponent<WarriorActor>().GetActorStat().MoveVelocity);
    }

    [Test]
    public void PlayerMoveTest()
    {
        //Input Move joystick left x to right
        inputDevice.UpdateWithStateTestCase(InputControlType.LeftStickX, true, 1000);

        //Character enters moving state
        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(testActor.GetComponent<TestActor>().GetState().GetType(), typeof(ActorMovingState));

        //Character should be moved
        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(true, testActor.GetComponent<TestActor>().hasMoved);

        //Character should be facing forward direction
        Assert.AreEqual(new Vector3(0, 90, 0), testActor.transform.GetChild(0).eulerAngles);

        //Input Move joystick left x to left
        inputDevice.UpdateWithValueTestCase(InputControlType.LeftStickX, -1, 1001);
        //Character should be facing backward direction
        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(new Vector3(0, 270, 0), testActor.transform.GetChild(0).eulerAngles);

        //After the move, the character should be stopped and Standing still
        inputDevice.UpdateWithStateTestCase(InputControlType.LeftStickX, false, 1002);

        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(testActor.GetComponent<TestActor>().GetState().GetType(), typeof(ActorStandingState));
    }
   
    [Test]
    public void PlayerJumpTest()
    {
        inputDevice.UpdateWithStateTestCase(InputControlType.Action3, true, 1000);

        testActor.GetComponent<TestActor>().HandleInput(inputDevice);

        Assert.AreEqual(1, testActor.GetComponent<TestActor>().hasJumped);
        Assert.AreEqual(typeof(ActorJumpState), testActor.GetComponent<TestActor>().GetState().GetType());

        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(2, testActor.GetComponent<TestActor>().hasJumped);
        Assert.AreEqual(typeof(ActorJumpState), testActor.GetComponent<TestActor>().GetState().GetType());

        testActor.GetComponent<TestActor>().HandleInput(inputDevice);
        Assert.AreEqual(2, testActor.GetComponent<TestActor>().hasJumped);
        Assert.AreEqual(typeof(ActorJumpState), testActor.GetComponent<TestActor>().GetState().GetType());
    }

    [Test]
    public void PlayerBlockTest()
    {
        inputDevice.UpdateWithStateTestCase(InputControlType.LeftTrigger, true, 1000);

        testActor.GetComponent<TestActor>().HandleInput(inputDevice);

        Assert.AreEqual(true, testActor.GetComponent<TestActor>().hasBlocked);
        Assert.AreEqual(typeof(ActorBlockState), testActor.GetComponent<TestActor>().GetState().GetType());
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    //[UnityTest]
    public IEnumerator PlayerMoveTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
