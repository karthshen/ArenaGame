using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerTestPlayerMode {

	[Test]
	public void PlayerTestPlayerModeSimplePasses() {
        // Use the Assert class to test conditions.

        ArcherActor archer = new ArcherActor();
        WarriorActor warrior = new WarriorActor();
        MageActor mage = new MageActor();

        Assert.AreEqual(archer.GetName(), "Archer");
        Assert.AreEqual(warrior.GetName(), "Warrior");
        Assert.AreEqual(mage.GetName(), "Mage");
    }

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator PlayerTestPlayerModeWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
