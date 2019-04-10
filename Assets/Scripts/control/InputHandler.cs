using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private int playerNum;
    private List<AActor> actors;

    public List<AActor> players;
    public GameObject actorInitializeLocation;

    private float restart_timer = 0f;
    private const float RESTART_TIMER = 5f;

    public int PlayerNum
    {
        get
        {
            return playerNum;
        }

        set
        {
            playerNum = value;
        }
    }

    public List<AActor> Actors
    {
        get
        {
            return actors;
        }

        set
        {
            actors = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Actors = new List<AActor>();

        //product vs debug
        if (Actors.Count == 0 && playerNum == 0)
        {
            AddPlayer(GameStageSetting.Player1Selection);
            AddPlayer(null);
        }
        else if(Actors.Count == 0 && playerNum == 1)
        {
            AddPlayer(null);
            AddPlayer(GameStageSetting.Player2Selection);
        }
        else
        {
            foreach (AActor player in players)
            {
                AddPlayer(player);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

        if(inputDevice != null)
        {
            HandleInput(inputDevice);
        }

        if(restart_timer > 0)
        {
            restart_timer -= Time.deltaTime;
            if (restart_timer <= 0)
            {
                SceneManager.LoadScene("Start");
            }
        }
        else if (Actors[playerNum].RespawnLives == 0 && restart_timer == 0)
        {
            restart_timer = 5f;

            AActor[] otherActors = GameObject.FindObjectsOfType<AActor>();

            foreach(AActor actor in otherActors)
            {
                if (actor.GetEntityId() != Actors[playerNum].GetEntityId())
                {
                    actor.Victory();
                }
            }
        }
    }

    void HandleInput(InputDevice inputDevice)
    {
        //Debug.Log("Current Player:" + playerNum);
        AActor actor = Actors[playerNum];

        if (!actor)
        {
            //If actor is not spawned yet
            Debug.Log("Actor not spawned for player: " + playerNum);
            return;
        }

        if (actor.GetState().GetType() != typeof(ActorDeathState))
        {
            actor.HandleInput(inputDevice);
        }
    }

    void AddPlayer(AActor actor)
    {
        Actors.Add(actor);
    }

    void AddPlayer(AActorEnum selectedActor)
    {
        GameObject actorToCreate = null;

        switch (selectedActor)
        {
            case AActorEnum.Warrior:
                actorToCreate = Object.Instantiate(Resources.Load("Warrior") as GameObject);
                break;
            case AActorEnum.Mage:
                actorToCreate = Object.Instantiate(Resources.Load("Mage") as GameObject);
                break;
            case AActorEnum.Archer:
                actorToCreate = Object.Instantiate(Resources.Load("Archer") as GameObject);
                break;
            default:
                break;
        }
        if (actorToCreate)
        {
            AddPlayer(actorToCreate.GetComponent<AActor>());
            if (Actors.Count == playerNum + 1)
            {
                InitializePlayerActor();
            }
        }
        else
        {
            throw new System.Exception("Actor failed to create");
        }
    }

    void RemovePlayer(AActor actor)
    {
        Actors.Remove(actor);
    }

    private void FixedUpdate()
    {

    }

    private void InitializePlayerActor()
    {
        Actors[playerNum].transform.position = actorInitializeLocation.transform.position;
        Actors[playerNum].transform.GetChild(0).transform.rotation = actorInitializeLocation.transform.rotation;
    }
}
