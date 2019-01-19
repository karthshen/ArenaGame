using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    private int playerNum;
    private List<AActor> actors;

    // Use this for initialization
    void Start()
    {
        actors = new List<AActor>();
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

        if(inputDevice != null)
        {
            HandleInput(inputDevice);
        }
    }

    void HandleInput(InputDevice inputDevice)
    {
        AActor actor = actors[playerNum];
        actor.HandleInput(inputDevice);
    }

    void AddPlayer(AActor actor)
    {
        actors.Add(actor);
    }

    void RemovePlayer(AActor actor)
    {
        actors.Remove(actor);
    }
}
