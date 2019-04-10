using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameGeneralTest
{
    static bool CloseEnoughForMe(double value1, double value2, double acceptableDifference)
    {
        return Math.Abs(value1 - value2) <= acceptableDifference;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void GravityConstantTest()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(-23.2653961f, Physics.gravity.y);
    }
}
